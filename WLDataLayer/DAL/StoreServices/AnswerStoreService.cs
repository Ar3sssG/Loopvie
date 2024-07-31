using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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

        public async Task<List<Answer>> GetUserLastAnswersAsync(int userId)
        {
            var filter = Builders<Answer>.Filter.Where(x => x.UserId == userId);
            var answers = await _mongoCollection.DistinctAsync<Answer>("WordId", filter);
            var sortedAnswers = await answers.ToListAsync();
            sortedAnswers.OrderByDescending(x => x.Id).Take(16).ToList();
            sortedAnswers.RemoveAt(0);
            return sortedAnswers;
        }
    }
}
