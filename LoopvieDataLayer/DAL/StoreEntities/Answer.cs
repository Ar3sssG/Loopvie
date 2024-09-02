using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LoopvieDataLayer.DAL.StoreEntities
{
    public class Answer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int UserId { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public string WordId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
