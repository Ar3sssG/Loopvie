using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoopvieBusinessLogic.Interfaces;
using LoopvieCommon.Models.Error;
using LoopvieCommon.Models.Request;
using LoopvieCommon.Models.Response;
using LoopvieDataLayer.Identity;

namespace LoopvieAPI.Controllers
{
    public class WordController : BaseController
    {
        private IWordManager _wordManager;
        public WordController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<IdentityController> logger, IWordManager wordManager)
            : base(userManager, signInManager, logger)
        {
            _wordManager = wordManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Get Word
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns>Word suggested by algorythm</returns>
        [HttpGet]
        [ProducesResponseType(typeof(WordResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWord(int difficulty)
        {
            User user = await _userManager.GetUserAsync(User);
            WordResponseModel response = await _wordManager.GetWordAsync(user.Id, difficulty);
            return Ok(response);
        }

        /// <summary>
        /// Gets Blitz Word
        /// </summary>
        /// <returns>Random word</returns>
        [HttpGet]
        [ProducesResponseType(typeof(WordResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBlitzWord(int difficulty)
        {
            WordResponseModel response = await _wordManager.GetBlitzWordAsync(difficulty);
            return Ok(response);
        }

        /// <summary>
        /// Submit Answer
        /// </summary>
        /// <returns>Answers response</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AnswerResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SubmitAnswer(AnswerRequestModel request)
        {
            User user = await _userManager.GetUserAsync(User);
            AnswerResponseModel response = await _wordManager.SubmitAnswerAsync(request, user.Id);
            return Ok(response);
        }

        /// <summary>
        /// Add new words to storage
        /// </summary>
        /// <returns>Ok</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer",Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddNewWords(List<WordRequestModel> words)
        {
            List<WordCreateResponseModel> response = await _wordManager.AddWordsAsync(words);
            return Ok(response);
        }
    }
}
