using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoopvieDataLayer.Identity;

namespace LoopvieAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger<BaseController> _logger;
        protected UserManager<User> _userManager;
        protected SignInManager<User> _signInManager;

        public BaseController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<BaseController> logger = null)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
    }
}
