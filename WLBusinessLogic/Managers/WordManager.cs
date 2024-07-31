using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WLBusinessLogic.Interfaces;
using WLCommon.Helpers;
using WLCommon.Models.Response.Word;
using WLDataLayer.DAL.Interfaces;
using WLDataLayer.DAL.StoreEntities;
using WLDataLayer.DAL.StoreServices;
using WLDataLayer.Identity;

namespace WLBusinessLogic.Managers
{
    public class WordManager : BaseManager, IWordManager
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly WordStoreService _wordStoreService;
        private readonly AnswerStoreService _answerStoreService;
        public WordManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, WordStoreService wordStoreService, AnswerStoreService answerStoreService) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _wordStoreService = wordStoreService;
            _answerStoreService = answerStoreService;
        }

        public async Task<WordResponseModel> GetWordAsync(int userId, int difficulty)
        {
            //Get the user's last 15 answers excluding the most recent one
            List<Answer> userAnswers = await _answerStoreService.GetUserLastAnswersAsync(userId);
            List<Word> words = await _wordStoreService.GetAsync();
            List<Word> userNewWords = words.Where(x => x.Difficulty == difficulty && !userAnswers.Select(y => y.WordId).Contains(x.Id)).ToList();
            WordResponseModel response;
            Random random = new Random();

            if (userAnswers.Any() && userAnswers.Count() > 15)
            {
                response = new WordResponseModel();
            }
            else
            {
                if (!userNewWords.Any())
                {
                    ExceptionHelper.ThrowResourseNotfound("not_found_new_words");
                }
                var selectedWord = userNewWords[random.Next(0, userNewWords.Count())];
                List<string> answerVariants = WordHelper.GetWordAnswersVariants(selectedWord.CorrectAnswer, selectedWord.WrongVariants);
                WordHelper.RandomizeList(answerVariants);
                response = new WordResponseModel
                {
                    Id = selectedWord.Id,
                    Text = selectedWord.Text,
                    Variants = answerVariants
                };
                
            }

            return response;
        }
    }
}
