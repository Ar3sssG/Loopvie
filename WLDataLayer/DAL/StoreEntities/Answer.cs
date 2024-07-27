using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WLDataLayer.DAL.StoreEntities
{
    public class Answer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
