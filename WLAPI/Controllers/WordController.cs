using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WLBusinessLogic.Interfaces;
using WLDataLayer.Identity;

namespace WLAPI.Controllers
{
    public class WordController : BaseController
    {
        private IWordManager _wordManager;
        public WordController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<IdentityController> logger,IWordManager wordManager)
            : base(userManager, signInManager, logger)
        {
            _wordManager = wordManager;
        }
    }
}
