using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata;
using WebApplication8.Models;
using IronPdf;
using System.Net;

namespace WebApplication8.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMongoCollection<DataWeb> _mongocoll;
        private readonly ILogger<HomeController> _logger;
        private string image;
        private string name;
        private string des;
        private string url;
        private string pdfurl;
        private string ozet;
        private string anahtar;
        private string kaynak;
        private string genre;
        private string publish;
        private string date;
        private string doinumber;
		List<DataWeb> datalist = new List<DataWeb>();
       
        
        private int i=1;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
            const string connectionUri = "mongodb+srv://naslimerusu:Mertbaba2149@webmongo.l1bfmaf.mongodb.net/?retryWrites=true&w=majority&appName=WebMongo";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            var client = new MongoClient(settings);

            var database = client.GetDatabase("WebScrapDB");
            _mongocoll = database.GetCollection<DataWeb>("Test");
        }

        public IActionResult Index()
        {
            //var filter = Builders<DataWeb>.Filter.Empty;
            //var result = _mongocoll.DeleteMany(filter);


            return View();
        }

        public IActionResult ScrapWeb(string site)
        {
            
            var website = new HtmlWeb();
            var html = website.Load("https://dergipark.org.tr/tr/search?q="+site+"&section=articles");
            var nodeElement = html.DocumentNode.SelectNodes("//div[@class='card article-card dp-card-outline']");
            //foreach(var selector in divSelect)

            //var selectdiv = html.DocumentNode.SelectNodes(selector);
            //if(selectdiv != null) 

            foreach (var node in nodeElement.Take(10))
            {

               
                var divNode = node.ChildNodes.FirstOrDefault(x => x.Name == "div");
                if (divNode != null)
                {
                    var h5Node = divNode.ChildNodes.FirstOrDefault(x => x.Name == "h5");
                    if (h5Node != null)
                    {


                        var aNode =h5Node.ChildNodes.FirstOrDefault(x => x.Name == "a");
                        if (aNode != null)
                        {
                            
                           var srcAttribute = aNode.Attributes.FirstOrDefault(x => x.Name == "href");
                                        if (srcAttribute != null)
                                        {
                                            url = srcAttribute.Value;
                                 HtmlDocument html1 = website.Load(srcAttribute.Value);

                                var nodeElement1 = html1.DocumentNode.SelectNodes("//div[@class='kt-portlet__body']");
                                if(nodeElement1 != null)
                                {

                                
                                foreach (var node1 in nodeElement1)
                                {
                                        var div2 = node1.ChildNodes.FirstOrDefault(x => x.Name == "div");

                                        if (div2 != null)
                                    {
                                        var a2 = div2.ChildNodes.FirstOrDefault(x => x.Name == "a");
                                        if (a2 != null)
                                        {
                                            var srcAttribute2 = a2.Attributes.FirstOrDefault(x => x.Name == "href");
                                            if (srcAttribute2 != null)
                                            {





                                                    
                                                    pdfurl = "https://dergipark.org.tr" + srcAttribute2.Value.ToString();
                                                   
                                                    
                                                    //i++;

                                                        
                                                }
                                        } 
                                            var div3 = div2.SelectNodes("//div[@class='tab-pane active ']").FirstOrDefault();
                                            if (div3 != null)
                                            {
                                                    var div4 = div3.ChildNodes.FirstOrDefault(y => y.Name == "div" );
                                                    if (div4 != null)
                                                    {


                                                    
                                                    var h3 = div4.SelectSingleNode("h3");
                                                    if(h3 != null)
                                                    {
                                                        name = h3.InnerText.Trim().ToString();
                                                    }
                                                   
                                                    }

                                                var inner = div3.SelectNodes("div");
                                                if (inner != null)
                                                {
                                                 foreach(var inner2 in inner)
                                                    {
                                                        var kontrol = inner2.GetAttributeValue("class", "");
                                                        if(kontrol.Equals("article-abstract data-section"))
                                                        {
                                                            var onlyp = inner2.SelectSingleNode("p").InnerText.Trim().ToString();
                                                            if (onlyp != null)
                                                            {
                                                                ozet = onlyp;
                                                            }
                                                        }

                                                        else if(kontrol.Equals("article-keywords data-section"))
                                                        {
                                                            var onlyp = inner2.SelectSingleNode("p").InnerText.Trim().ToString();
                                                            if (onlyp != null)
                                                            {
                                                                anahtar = onlyp;
                                                            }
                                                        }

                                                        else if(kontrol.Equals("article-citations data-section"))
                                                        {
                                                            var onlyp = inner2.SelectSingleNode("div").InnerText.Trim().ToString();
                                                            if(onlyp!=null)
                                                            {
                                                                kaynak = onlyp;
                                                            }
                                                             
                                                        }
                                                        else if(kontrol.Equals("article-doi data-section"))
                                                        {
                                                            doinumber = inner2.InnerText.Trim().ToString(); 
                                                        }

                                                    }
                                                }

                                                Thread.Sleep(2500);
                                                WebClient wbclient = new WebClient();
                                                wbclient.DownloadFile(pdfurl, name + ".pdf");



                                            }
                                        }
                                    }
                                }

                                var nodeelement2 = html1.DocumentNode.SelectNodes("//div[@class='kt-portlet article-details']");
                                if (nodeelement2 != null)
                                {
                                    foreach (var nodeelement3 in nodeelement2)
                                    {
                                       var tablediv = nodeelement3.ChildNodes.FirstOrDefault(a=> a.Name == "div");
                                        if(tablediv != null)
                                        {
                                            var table = tablediv.ChildNodes.FirstOrDefault(a=> a.Name == "table");
                                            if(table != null)
                                            {
                                                var Bolumbul = table.SelectSingleNode("tr[th='Bölüm']");
                                                if(Bolumbul != null)
                                                {
                                                    var onlytd = Bolumbul.SelectSingleNode("td").InnerText.Trim();
                                                    genre = onlytd.ToString();
                                                }
                                                var Yazarbul = table.SelectSingleNode("tr[th='Yazarlar']");
                                                if(Yazarbul!=null)
                                                {
                                                    var onlytd = Yazarbul.SelectSingleNode("td").InnerText.Trim();
                                                    publish = onlytd.ToString();
                                                }
                                                var Tarihbul = table.SelectSingleNode("tr[th='Yayımlanma Tarihi']");
                                                if(Tarihbul!=null)
                                                {
                                                    var onlytd = Tarihbul.SelectSingleNode("td").InnerText.Trim();
                                                    date = onlytd.ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                                var dataliste = new DataWeb
                                            {
                                                Id = ObjectId.GenerateNewId(),
                                                URL = url,
                                                Name = name,
                                                PublisherName = publish,
                                                Ozet = ozet,
                                                Date = date,
                                                AnahtarKelimeler = anahtar,
                                                Referanslar = kaynak,
                                                Genre = genre,
                                                doiNumber = doinumber,
                                                PDFURL = pdfurl,
                                                Description = "DergiPark"

                                            };
                                            _mongocoll.InsertOne(dataliste);
                                            datalist.Add(dataliste);
                                        }
                                    
                                
                            
                        }
                    }
                }
            }
            return View("Index", datalist);

        }





        public IActionResult Detail(ObjectId id)
        {
            var filtre = Builders<DataWeb>.Filter.Eq("_id",id);
            var sec = _mongocoll.Find(filtre).FirstOrDefault();
            
            return View("Detail",sec);
        }

    }



            
            
        }
    
