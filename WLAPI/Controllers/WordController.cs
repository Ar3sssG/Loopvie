﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WLBusinessLogic.Interfaces;
using WLCommon.Models.Error;
using WLCommon.Models.Request;
using WLCommon.Models.Response;
using WLDataLayer.Identity;

namespace WLAPI.Controllers
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

    }
}
