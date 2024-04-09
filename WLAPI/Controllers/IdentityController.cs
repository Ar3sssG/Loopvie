using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WLBusinessLogic.Interfaces;
using WLCommon.Constants;
using WLCommon.Models.Manager;
using WLCommon.Models.Request;
using WLCommon.Models.Response;
using WLDataLayer.Identity;

namespace WLAPI.Controllers
{
    public class IdentityController : BaseController
    {
        private IIdentityManager _identityManager;
        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<IdentityController> logger, IIdentityManager identityManager)
            : base(userManager, signInManager, logger)
        {
            _identityManager = identityManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponseModel), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AuthenticateUser(AuthRequestModel requestModel)
        {
            AuthResponseModel response;
            User user = await _userManager.FindByEmailAsync(requestModel.Username);
            if (user == null)
            {
                UnauthorizedResponseModel unauthorizedResponseModel = new UnauthorizedResponseModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = AuthErrorConstants.UserNotFound
                };

                return BadRequest(unauthorizedResponseModel);
            }

            response = await Authenticate(user, requestModel.Password);

            if (!response.IsAuthorized)
            {
                UnauthorizedResponseModel unauthorizedResponseModel = new UnauthorizedResponseModel
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = AuthErrorConstants.IncorrectUsernameOrPass
                };

                return Unauthorized(unauthorizedResponseModel);
            }

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponseModel), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RegisterUser(RegisterRequestModel requestModel)
        {
            try
            {
                User user = new User
                {
                    FirstName = requestModel.FirstName,
                    LastName = requestModel.LastName,
                    UserName = requestModel.Email,
                    Email = requestModel.Email,
                    NormalizedUserName = requestModel.Email.ToUpper(),
                    NormalizedEmail = requestModel.Email.ToUpper(),
                };

                var result = await _userManager.CreateAsync(user, requestModel.Password);

                if (result.Succeeded)
                {
                    User createdUser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(createdUser, "User");
                    AuthResponseModel response = await Authenticate(createdUser, requestModel.Password);
                    if (!response.IsAuthorized)
                    {
                        UnauthorizedResponseModel unauthorizedResponseModel = new UnauthorizedResponseModel
                        {
                            StatusCode = StatusCodes.Status401Unauthorized,
                            Message = AuthErrorConstants.IncorrectUsernameOrPass
                        };

                        return Unauthorized(unauthorizedResponseModel);
                    }

                    return Ok(response);
                }
                else
                {
                    UnauthorizedResponseModel unauthorizedResponseModel = new UnauthorizedResponseModel
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = string.Join(",", result.Errors.Select(x => x.Description))
                    };

                    return Unauthorized(unauthorizedResponseModel);
                }
            }
            catch (Exception ex)
            {
                UnauthorizedResponseModel unauthorizedResponseModel = new UnauthorizedResponseModel
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "unauthorized"
                };

                return Unauthorized(unauthorizedResponseModel);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponseModel), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel requestModel)
        {
            AuthResponseModel response = new AuthResponseModel();

            try
            {
                User user = await _identityManager.GetUserByRefreshToken(requestModel.RefreshToken);
                response = await Authenticate(user, string.Empty, true);
                if (!response.IsAuthorized)
                {
                    UnauthorizedResponseModel unauthorizedResponseModel = new UnauthorizedResponseModel
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = AuthErrorConstants.IncorrectUsernameOrPass
                    };

                    return Unauthorized(unauthorizedResponseModel);
                }
            }
            catch (Exception ex)
            {
                UnauthorizedResponseModel unauthorizedResponseModel = new UnauthorizedResponseModel
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };

                return BadRequest(unauthorizedResponseModel);
            }

            return Ok(response);
        }

        [NonAction]
        private async Task<AuthResponseModel> Authenticate(User user, string password, bool isRefreshTokenRequest = false)
        {
            AuthResponseModel responseModel = new AuthResponseModel();
            try
            {
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, string.Join(",",roles)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                Microsoft.AspNetCore.Identity.SignInResult signInResult = new Microsoft.AspNetCore.Identity.SignInResult();

                if (isRefreshTokenRequest)
                {
                    await _signInManager.SignInAsync(user, true);
                }
                else
                {
                    signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                }

                if (!signInResult.Succeeded)
                {
                    responseModel.IsAuthorized = false;
                    return responseModel;
                }
                else
                {
                    responseModel.IsAuthorized = true;
                }

                JwtSecurityToken jwt = new JwtSecurityToken(
                        issuer: AuthConstants.ISSUER,
                        audience: AuthConstants.AUDIENCE,
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                string accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("type", "refresh") }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityRefreshKey(), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var refreshSecurityToken = tokenHandler.CreateToken(tokenDescriptor);

                string refreshToken = tokenHandler.WriteToken(refreshSecurityToken);

                RefreshTokenModel refreshTokenModel = new RefreshTokenModel
                {
                    UserId = user.Id,
                    ExpireDate = tokenDescriptor.Expires.Value,
                    Token = refreshToken,
                };

                await _identityManager.AddRefreshToken(refreshTokenModel);

                responseModel.AccessToken = $"Bearer {accessToken}";
                responseModel.RefreshToken = refreshToken;
                responseModel.Username = user.Email;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                responseModel.IsAuthorized = false;
                return responseModel;
            }

            return responseModel;
        }
    }
}
