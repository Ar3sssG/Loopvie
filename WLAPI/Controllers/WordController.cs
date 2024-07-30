using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WLBusinessLogic.Interfaces;
using WLCommon.Models.Response.Word;
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
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetWord()
        {
            User user = await _userManager.GetUserAsync(User);
            WordResponseModel word= await _wordManager.GetWordAsync(user.Id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAnswer()
        {
            return Ok(await Task.FromResult(0));
        }

        [HttpGet]
        public async Task<IActionResult> GetBlitzWord()
        {
            return Ok(await Task.FromResult(0));
        }
    }
}
