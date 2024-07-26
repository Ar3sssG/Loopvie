using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WLDataLayer.DAL.StoreServices.Interfaces;
using WLDataLayer.DAL.StoreServices.StoresSettings;

namespace WLDataLayer.DAL.StoreServices
{
    public class AnswerStoreService : IAnswerStoreService
    {
        //**********************TODO**************************\\
        //**********************TODO**************************\\
        //**********************TODO**************************\\
        //**********************TODO**************************\\
        private readonly IMongoCollection<dynamic> _answerCollection; 

        public AnswerStoreService(IOptions<AnswerStoreServiceSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);

            var database = client.GetDatabase(options.Value.StoreName);

            _answerCollection = database.GetCollection<dynamic>(options.Value.CollectionName);
        }

        public async Task<dynamic> GetAsync()
        {
            return await _answerCollection.Find(_ => true).ToListAsync();
        }

        public async Task<dynamic> GetAsync()
        {
            return await _answerCollection.Find(_ => true).ToListAsync();
        }
    }
}
