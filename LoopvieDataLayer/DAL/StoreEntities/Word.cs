using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LoopvieDataLayer.DAL.StoreEntities
{
    public class Word
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> WrongVariants { get; set; }
        public int Difficulty { get; set; }
    }
}
