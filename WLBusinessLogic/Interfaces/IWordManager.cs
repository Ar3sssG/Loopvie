
using WLDataLayer.DAL.StoreEntities;

namespace WLBusinessLogic.Interfaces
{
    public interface IWordManager
    {
        Task<Word> GetWordAsync(int userId);
    }
}
