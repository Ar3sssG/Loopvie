using LoopvieCommon.Models.Request;
using LoopvieCommon.Models.Response;

namespace LoopvieBusinessLogic.Interfaces
{
    public interface IWordManager
    {
        Task<WordResponseModel> GetWordAsync(int userId, int difficulty);
        Task<WordResponseModel> GetBlitzWordAsync(int difficulty, bool isRandomDifficulty = false);
        Task<AnswerResponseModel> SubmitAnswerAsync(AnswerRequestModel request, int userId);
        Task<List<WordCreateResponseModel>> AddWordsAsync(List<WordRequestModel> words);
    }
}
