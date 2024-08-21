using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WLBusinessLogic.Interfaces;
using WLCommon.Models.Error;
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

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>Access and refresh tokens</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Authenticate(AuthRequestModel requestModel)
        {
            AuthResponseModel response = await _identityManager.AuthenticateUserAsync(requestModel); 
            return Ok(response);
        }


        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>Access and refresh tokens</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Register(RegisterRequestModel requestModel)
        {
            AuthResponseModel response = await _identityManager.RegisterUserAsync(requestModel);
            return Ok(response);
        }

        /// <summary>
        /// Authenticate by RefreshToken
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel requestModel)
        {
            AuthResponseModel response = await _identityManager.AuthenticateByRefreshTokenAsync(requestModel);
            return Ok(response);
        }

        /// <summary>
        /// Check is token expired
        /// </summary>
        /// <param name="token"></param>
        /// <returns>true or false</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CheckToken([FromHeader(Name = "Authorization")] string token)
        {
            bool response = await _identityManager.CheckTokenAsync(token); 
            return Ok(response);
        }
    }
}
