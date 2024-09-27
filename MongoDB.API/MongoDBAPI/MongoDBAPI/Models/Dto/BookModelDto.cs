using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBAPI.Models.Dto
{
    public class BookModelDto
    {
        public string title { get; set; }
        public string author { get; set; }
        public int pages { get; set; }
        public int rating { get; set; }
        public List<string> genres { get; set; } = new List<string>();
        public List<ReviewModel> reviews { get; set; } = new List<ReviewModel>();
    }
}
