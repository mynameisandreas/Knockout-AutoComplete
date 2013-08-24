using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Knockout_Autocomplete.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Knockout_Autocomplete.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<SearchResult> _searchResults;

        public HomeController()
        {
            _searchResults = new List<SearchResult>()
                {
                    new SearchResult("Arnoldo", "Mullenax"),
                    new SearchResult("Jed", "Prada"),  
                    new SearchResult("Lidia", "Besaw"),  
                    new SearchResult("Melina", "Burbach"),  
                    new SearchResult("Rubye", "Mehring"),  
                    new SearchResult("Charley", "Hager"),  
                    new SearchResult("Amiee", "Debolt"),  
                    new SearchResult("Marg", "Rodney"),  
                    new SearchResult("Narcisa", "Nuttall"),  
                    new SearchResult("Felice", "Lovern"), 
                    new SearchResult("Cristine", "Hammersley"),  
                    new SearchResult("Sacha", "Brown"),
                    new SearchResult("Seth", "Muto"),
                    new SearchResult("Carlton", "Loving"),
                    new SearchResult("Candice", "Martelli"),
                    new SearchResult("Yoshie", "Drolet"),
                    new SearchResult("Frederica", "Stgeorge"),
                    new SearchResult("Travis", "Hux"),
                    new SearchResult("Daisey", "Poe"),
                    new SearchResult("Emmanuel", "Huntoon"),
                    new SearchResult("Georgia", "Childress"),
                    new SearchResult("Alva", "Suazo"),
                    new SearchResult("Reagan", "Shroyer"),
                    new SearchResult("Quinton", "Fuhrman"),
                    new SearchResult("Crista", "Bunge"),
                    new SearchResult("Francis", "Urquhart"),
                    new SearchResult("Newton", "Gassett"),
                    new SearchResult("Lue", "Casazza"),
                    new SearchResult("Latonya", "Clift"),
                    new SearchResult("Arica", "Paton"),
                    new SearchResult("Shanta", "Dowling"),
                    new SearchResult("Shiloh", "Cambron"),
                    new SearchResult("Jason", "Blackburn"),
                    new SearchResult("Amado", "Thames"),
                    new SearchResult("Janis", "Cloer"),
                    new SearchResult("Micah", "Mcmorris"),
                    new SearchResult("Earlene", "Isenhour"),
                    new SearchResult("Ute", "Coupe"),
                    new SearchResult("Casey", "Dano"),
                    new SearchResult("Bret", "Michaelis"),
                    new SearchResult("Rosalva", "Mclennan"),
                    new SearchResult("Elvina", "Warr"),
                    new SearchResult("Chrystal", "Strawn"),
                    new SearchResult("Alex", "Schaller"),
                    new SearchResult("Bryce", "Peralta"),
                    new SearchResult("Delila", "Tuller"),
                    new SearchResult("Louann", "Manke"),
                    new SearchResult("Hanh", "Lockwood"),
                    new SearchResult("Carson", "Fuller"),
                    new SearchResult("Dorian", "Dougan")
                };
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FetchSearchResults(string searchString)
        {
            var results =
                _searchResults.Where(x => x.FirstName.ToLower().Contains(searchString.ToLower()))
                              .OrderBy(s => s.FirstName);

            return Content(JsonConvert.SerializeObject(results, new JsonSerializerSettings{ContractResolver = new CamelCasePropertyNamesContractResolver()}));
        }
    }
}
