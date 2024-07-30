using Microsoft.Extensions.Options;
using WLDataLayer.DAL.StoreEntities;
using WLDataLayer.DAL.StoreServices.Interfaces;
using WLDataLayer.DAL.StoreServices.StoresSettings;

namespace WLDataLayer.DAL.StoreServices
{
    public class AnswerStoreService : BaseStoreService<Answer, AnswerStoreServiceSettings>, IAnswerStoreService
    {
        public AnswerStoreService(IOptions<AnswerStoreServiceSettings> options) : base(options)
        {
        }

    }
}
