using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using WLDataLayer.DAL.DBContext;
using WLDataLayer.DAL.StoreEntities;
using WLDataLayer.DAL.StoreServices.Interfaces;
using WLDataLayer.DAL.StoreServices.StoresSettings;

namespace WLDataLayer.DAL.StoreServices
{
    public class WordStoreService : IWordStoreService
    {
        private readonly IMongoCollection<Word> _wordCollection;

        public WordStoreService(IOptions<WordStoreServiceSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);

            var database = client.GetDatabase(options.Value.StoreName);

            _wordCollection = database.GetCollection<Word>(options.Value.CollectionName);
        }

        public async Task<List<Word>> GetAsync()
        {
            return await _wordCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Word> GetAsync(string id)
        {
            return await _wordCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Word word)
        {
            await _wordCollection.InsertOneAsync(word);
        }

        public async Task UpdateAsync(string id, Word word)
        {
            await _wordCollection.ReplaceOneAsync(x => x.Id == id, word);
        }

        public async Task RemoveAsync(string id)
        {
            await _wordCollection.DeleteOneAsync(x => x.Id == id);
        }
        /// <summary>
        /// //////////////////////TODOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async IQueryable<Word> Get(Expression<Func<Word, bool>> predicate)
        {
            IQueryable<Word> set = await _wordCollection.AsQueryable();

            if (predicate == null)
            {
                return set;
            }

            return set.Where(predicate);
        }
    }
}
