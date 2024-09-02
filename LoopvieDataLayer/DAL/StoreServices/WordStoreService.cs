using Microsoft.Extensions.Options;
using WLDataLayer.DAL.StoreEntities;
using WLDataLayer.DAL.StoreServices.Interfaces;
using WLDataLayer.DAL.StoreServices.StoresSettings;

namespace WLDataLayer.DAL.StoreServices
{
    public class WordStoreService : BaseStoreService<Word, WordStoreServiceSettings>, IWordStoreService
    {
        public WordStoreService(IOptions<WordStoreServiceSettings> options) : base(options)
        {
        }
        
    }
}
