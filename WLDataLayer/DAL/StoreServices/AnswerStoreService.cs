using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WLDataLayer.DAL.StoreEntities;
using WLDataLayer.DAL.StoreServices.Interfaces;
using WLDataLayer.DAL.StoreServices.StoresSettings;

namespace WLDataLayer.DAL.StoreServices
{
    public class AnswerStoreService : IAnswerStoreService
    {
        private readonly IMongoCollection<Answer> _answerCollection;

        public AnswerStoreService(IOptions<AnswerStoreServiceSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);

            var database = client.GetDatabase(options.Value.StoreName);

            _answerCollection = database.GetCollection<Answer>(options.Value.CollectionName);
        }

        public async Task<List<Answer>> GetAsync()
        {
            return await _answerCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Answer> GetAsync(string id)
        {
            return await _answerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Answer answer)
        {
            await _answerCollection.InsertOneAsync(answer);
        }

        public async Task UpdateAsync(string id, Answer answer)
        {
            await _answerCollection.ReplaceOneAsync(x => x.Id == id, answer);
        }

        public async Task RemoveAsync(string id)
        {
            await _answerCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
