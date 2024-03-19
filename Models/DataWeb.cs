using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication8.Models
{
    public class DataWeb
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        public string PublisherName { get; set; }
        
        public string AnahtarKelimeler {  get; set; }
        
        public string Ozet {  get; set; }

        public string Date { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public string Referanslar { get; set; }

        public string doiNumber { get; set; }
        public string URL { get; set; }

        public string PDFURL { get; set; }
    }
}
