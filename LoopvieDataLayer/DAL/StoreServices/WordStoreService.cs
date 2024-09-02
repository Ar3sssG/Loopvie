using Microsoft.Extensions.Options;
using LoopvieDataLayer.DAL.StoreEntities;
using LoopvieDataLayer.DAL.StoreServices.Interfaces;
using LoopvieDataLayer.DAL.StoreServices.StoresSettings;

namespace LoopvieDataLayer.DAL.StoreServices
{
    public class WordStoreService : BaseStoreService<Word, WordStoreServiceSettings>, IWordStoreService
    {
        public WordStoreService(IOptions<WordStoreServiceSettings> options) : base(options)
        {
        }
        
    }
}
