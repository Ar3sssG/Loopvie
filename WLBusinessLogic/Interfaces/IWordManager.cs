using WLCommon.Models.Response.Word;

namespace WLBusinessLogic.Interfaces
{
    public interface IWordManager
    {
        Task<WordResponseModel> GetWordAsync(int userId, int difficulty);
    }
}
