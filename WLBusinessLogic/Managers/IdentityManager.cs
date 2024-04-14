using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WLBusinessLogic.Interfaces;
using WLCommon.Constants;
using WLCommon.Helpers;
using WLCommon.Models.Manager;
using WLCommon.Models.Request;
using WLCommon.Models.Response;
using WLDataLayer.DAL.Entities;
using WLDataLayer.DAL.Interfaces;
using WLDataLayer.Identity;

namespace WLBusinessLogic.Managers
{
    public class IdentityManager : BaseManager, IIdentityManager
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public IdentityManager(IUnitOfWork unitOfWork,IMapper mapper,UserManager<User> userManager,SignInManager<User> signInManager) : base(unitOfWork,mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var token = await _unitOfWork.RefreshTokenRepository.Get(x => x.Token == refreshToken).Include(x => x.User).FirstOrDefaultAsync();

            if (token == null)
            {
                throw new Exception(AuthErrorConstants.InvalidRefreshToken);
            }

            if (token.ExpireDate < DateTime.UtcNow)
            {
                throw new Exception(AuthErrorConstants.ExpiredRefreshToken);
            }

            User user = token.User;

            return user;
        }

        public async Task<AuthResponseModel> AuthenticateUserAsync(AuthRequestModel requestModel)
        {
            AuthResponseModel response = new();
            User user = await _userManager.FindByEmailAsync(requestModel.Username);
            if (user != null)
            {
                response = await Authenticate(user, requestModel.Password);
                if (!response.IsAuthorized)
                {
                    ExceptionHelper.ThrowUnauthorized(AuthErrorConstants.IncorrectUsernameOrPass);
                }
            }
            else
            {
                ExceptionHelper.ThrowBadRequest(AuthErrorConstants.UserNotFound);
            }

            return response;
        }

        public async Task<AuthResponseModel> RegisterUserAsync(RegisterRequestModel requestModel)
        {
            AuthResponseModel response = new();
            await _unitOfWork.BeginTransaction();

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

                response = await Authenticate(createdUser, requestModel.Password);
                if (!response.IsAuthorized)
                {
                    await _unitOfWork.RollBackTrasnactionAsync();
                    ExceptionHelper.ThrowUnauthorized();
                }

                await _unitOfWork.CommitTrasnactionAsync();
                return response;
            }
            else
            {
                await _unitOfWork.RollBackTrasnactionAsync();
                ExceptionHelper.ThrowUnauthorized(string.Join(",", result.Errors.Select(x => x.Description)));
                return response;
            }
        }

        public async Task<AuthResponseModel> AuthenticateByRefreshTokenAsync(RefreshTokenRequestModel requestModel)
        {
            AuthResponseModel response = new();
            try
            {
                await _unitOfWork.BeginTransaction();
                User user = await GetUserByRefreshTokenAsync(requestModel.RefreshToken);
                response = await Authenticate(user, string.Empty, true);

                if (!response.IsAuthorized)
                {
                    ExceptionHelper.ThrowUnauthorized();
                }

                await _unitOfWork.CommitTrasnactionAsync();
                
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTrasnactionAsync();
                ExceptionHelper.ThrowThrowedException(ex);
            }

            return response;
        }

        #region PrivateMethods

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

                await AddRefreshToken(refreshTokenModel);

                responseModel.AccessToken = $"Bearer {accessToken}";
                responseModel.RefreshToken = refreshToken;
                responseModel.Username = user.Email;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                responseModel.IsAuthorized = false;
                return responseModel;
            }

            return responseModel;
        }

        private async Task AddRefreshToken(RefreshTokenModel model)
        {
            //check if user have token delete
            var oldToken = await _unitOfWork.RefreshTokenRepository.Get(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            if (oldToken != null)
            {
                _unitOfWork.RefreshTokenRepository.Delete(oldToken);
            }

            RefreshToken refreshToken = new RefreshToken
            {
                UserId = model.UserId,
                Token = model.Token,
                ExpireDate = model.ExpireDate,
                CreatedDate = DateTime.UtcNow,
            };

            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}
