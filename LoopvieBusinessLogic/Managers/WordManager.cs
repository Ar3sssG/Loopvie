using AutoMapper;
using Microsoft.AspNetCore.Identity;
using LoopvieBusinessLogic.Interfaces;
using LoopvieCommon.Helpers;
using LoopvieCommon.Models.Request;
using LoopvieCommon.Models.Response;
using LoopvieDataLayer.DAL.Interfaces;
using LoopvieDataLayer.DAL.StoreEntities;
using LoopvieDataLayer.DAL.StoreServices;
using LoopvieDataLayer.Identity;

namespace LoopvieBusinessLogic.Managers
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
            Word selectedWord = null;

            if (userAnswers.Any() && userAnswers.Count() > 15)
            {
                //Categorizing answers
                List<Answer> correctAnswers = userAnswers.Where(x => x.IsCorrect).ToList();
                List<Answer> incorrectAnswers = userAnswers.Where(x => !x.IsCorrect).ToList();

                List<Answer> slowCorrectAnswers = new List<Answer>();
                List<Answer> fastCorrectAnswers = new List<Answer>();

                if (correctAnswers.Any())
                {
                    //checking speed by getting avg time spent on correct answers
                    long averageMilliseconds = Convert.ToInt64(correctAnswers.Select(x => x.TimeSpent.TotalMilliseconds).Sum() / correctAnswers.Count);
                    slowCorrectAnswers = correctAnswers.Where(x => x.TimeSpent.TotalMilliseconds > averageMilliseconds + 2000).ToList();
                    if (slowCorrectAnswers.Any())
                    {
                        fastCorrectAnswers = correctAnswers.Except(slowCorrectAnswers).ToList();
                    }
                }

                //***************************************TODO*****************************************\\
                // Determine the priority for selecting a word
                //need to do ranges variable 
                //like if there is no have any incorrect answers select range which often returns new words
                //make normal variable db stored ranges 
                int[,] array = new int[4, 2] { { 51, 100 }, { 26, 50 }, { 8, 25 }, { 1, 7 } };
                //getting random number to 
                int randomNumber = random.Next(0, 101);
                int priority = 0;
                //getting priority by selected number
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        if (j == 1)
                        {
                            continue;
                        }
                        if (randomNumber > array[i, j] && randomNumber < array[i, j + 1])
                        {
                            priority = i++;
                        }
                    }
                }
                //************************************************************************************\\

                // Select a word based on the priority
                if (priority == 1 && incorrectAnswers.Any())
                {
                    int randomValue = random.Next(0, incorrectAnswers.Count);
                    selectedWord = words.Where(x => x.Id == incorrectAnswers[randomValue].WordId).FirstOrDefault();
                }
                else if (priority == 2 && userNewWords.Any())
                {
                    int randomValue = random.Next(0, userNewWords.Count);
                    selectedWord = userNewWords[randomValue];
                }
                else if (priority == 3 && slowCorrectAnswers.Any())
                {
                    int randomValue = random.Next(0, slowCorrectAnswers.Count);
                    selectedWord = words.FirstOrDefault(x => x.Id == slowCorrectAnswers[randomValue].WordId);
                }
                else if (priority == 4 && fastCorrectAnswers.Any())
                {
                    int randomValue = random.Next(0, fastCorrectAnswers.Count);
                    selectedWord = words.FirstOrDefault(x => x.Id == fastCorrectAnswers[randomValue].WordId);
                }

                if (selectedWord == null)
                {
                    selectedWord = userNewWords.FirstOrDefault();
                }

            }
            else
            {
                if (!userNewWords.Any())
                {
                    ExceptionHelper.ThrowResourseNotfound("not_found_new_words");
                }
                selectedWord = userNewWords[random.Next(0, userNewWords.Count())];
            }

            List<string> answerVariants = WordHelper.GetWordAnswersVariants(selectedWord.CorrectAnswer, selectedWord.WrongVariants);
            WordHelper.RandomizeList(answerVariants);
            response = new WordResponseModel
            {
                Id = selectedWord.Id,
                Text = selectedWord.Text,
                Variants = answerVariants
            };

            return response;
        }

        public async Task<WordResponseModel> GetBlitzWordAsync(int difficulty, bool isRandomDifficulty = false)
        {
            Random random = new Random();
            List<Word> words = await _wordStoreService.GetAsync();
            Word selectedWord;
            WordResponseModel response;

            if (isRandomDifficulty)
            {
                selectedWord = words[random.Next(0, words.Count)];
            }
            else
            {
                List<Word> wordDiffList = words.Where(x => x.Difficulty == difficulty).ToList();
                selectedWord = wordDiffList[random.Next(0, wordDiffList.Count)];
            }

            List<string> answerVariants = WordHelper.GetWordAnswersVariants(selectedWord.CorrectAnswer, selectedWord.WrongVariants);
            WordHelper.RandomizeList(answerVariants);

            response = new WordResponseModel
            {
                Id = selectedWord.Id,
                Text = selectedWord.Text,
                Variants = answerVariants
            };

            return response;
        }

        public async Task<AnswerResponseModel> SubmitAnswerAsync(AnswerRequestModel request, int userId)
        {
            List<Word> wordList = await _wordStoreService.GetAsync(x => x.Id == request.WordId);
            Word word = wordList.FirstOrDefault();

            if (word == null)
            {
                ExceptionHelper.ThrowResourseNotfound("word_not_found");
            }

            bool isCorrect = word.CorrectAnswer == request.WordAnswer;

            Answer answer = new Answer
            {
                UserId = userId,
                TimeSpent = request.TimeSpent,
                WordId = word.Id,
                IsCorrect = isCorrect
            };

            await _answerStoreService.AddAsync(answer);

            return new AnswerResponseModel { WordId = word.Id, IsCorrect = isCorrect };
        }

        public async Task<List<WordCreateResponseModel>> AddWordsAsync(List<WordRequestModel> words)
        {
            List<WordCreateResponseModel> response = new List<WordCreateResponseModel>();
            List<Word> existingWords = await _wordStoreService.GetAsync();
            foreach (WordRequestModel item in words)
            {
                if (existingWords.Select(x => x.Text).Contains(item.Word))
                {
                    response.Add(new WordCreateResponseModel { Word = item.Word, Message = "word_already_exists" });
                }
                try
                {
                    Word newWord = _mapper.Map<Word>(item);

                    await _wordStoreService.AddAsync(newWord);
                }
                catch (Exception ex)
                {
                    response.Add(new WordCreateResponseModel { Word = item.Word, Message = ex.Message });
                }
            }

            return response;
        }
    }
}
