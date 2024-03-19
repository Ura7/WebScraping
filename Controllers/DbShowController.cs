using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class DbShowController : Controller
    {
        private readonly IMongoCollection<DataWeb> _mongoCollection;

        //private readonly ElasticSearch _elastic;

        public DbShowController() 
        {
            const string connectionUri = "mongodb+srv://naslimerusu:Mertbaba2149@webmongo.l1bfmaf.mongodb.net/?retryWrites=true&w=majority&appName=WebMongo";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            var client = new MongoClient(settings);

            var database = client.GetDatabase("WebScrapDB");
            _mongoCollection= database.GetCollection<DataWeb>("Test");
            
        }

        public IActionResult Index(string veri)
        {
            var filtre = Builders<DataWeb>.Filter.Empty;
            var data = _mongoCollection.Find(filtre).ToList();

            //var search = _elastic.Search<DataWeb>(veri);
            return View(data);
        }

       
    }
}
