using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBAPI.Models
{
    public class BookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int pages { get; set; }
        public int rating { get; set; }
        public List<string> genres { get; set; } = new List<string>();
        public List<ReviewModel> reviews { get; set; } = new List<ReviewModel>();
    }

}
