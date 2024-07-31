using WLCommon.Models.Response;

namespace WLBusinessLogic.Interfaces
{
    public interface IWordManager
    {
        Task<WordResponseModel> GetWordAsync(int userId, int difficulty);
        Task<WordResponseModel> GetBlitzWordAsync(int difficulty, bool isRandomDifficulty = false);
    }
}
