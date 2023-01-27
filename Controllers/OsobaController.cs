using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using AspWebApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AspWebApp.Controllers
{
    public class OsobaController : ApiController
    {

        private readonly string _path = $"C:\\Users\\Silvio\\Source\\repos\\AspWebApp\\assets\\lib\\Osoba.json";


        // GET: api/Osoba
        public List<OsobaModel> Get()
        {
            StreamReader streamReader = new StreamReader(_path);
            string json = "";
            using (streamReader)
            {
                json = streamReader.ReadToEnd();
            }
            JObject jsonObject = JObject.Parse(json);
            var osobe = jsonObject["osoba"].ToList();
            List<OsobaModel> popisOsoba = new List<OsobaModel>();            

            DateTime date = new DateTime();
            string formatted = "";
            SpolController spol = new SpolController();
            List<Spol> spolovi = spol.Get();
 
            string spolNaziv = "";
            int nSpol = 0;
            
            for (int i = 0; i < osobe.Count(); i++)
            {
                date = (DateTime)osobe[i]["DatumRodenja"];
                date = (DateTime)osobe[i]["DatumRodenja"];
                formatted = Convert.ToDateTime(date).ToString("dd/MM/yyyy");

                nSpol = (int)osobe[i]["Spol"];
                spolNaziv = spolovi.Where(p => p.Id == nSpol).Select(p => p.Naziv).FirstOrDefault();

            popisOsoba.Add(new OsobaModel()
                {
                    Id = (Guid)osobe[i]["Id"],
                    Ime = (string)osobe[i]["Ime"],
                    Prezime = (string)osobe[i]["Prezime"],
                    DatumRodenja = formatted,
                    Spol = spolNaziv,
                    Telefon = (string)osobe[i]["Telefon"],
                    Adresa = (string)osobe[i]["Adresa"]
                });
            }
            return popisOsoba;
        }


        // GET: api/Osoba/5
        public List<Osoba> Get(Guid id)
        {
            StreamReader streamReader = new StreamReader(_path);
            string json = "";
            using (streamReader)
            {
                json = streamReader.ReadToEnd();
            }
            JObject jsonObject = JObject.Parse(json);
            var osobe = jsonObject["osoba"].ToList();
            List<Osoba> popisOsoba = new List<Osoba>();

            DateTime date = new DateTime();
            string formatted = "";

            for (int i = 0; i < osobe.Count(); i++)
            {
                date = (DateTime)osobe[i]["DatumRodenja"];
                formatted = Convert.ToDateTime(date).ToString("MM/dd/yyyy");

                if ((Guid)osobe[i]["Id"] == id)
                {

                    popisOsoba.Add(new Osoba()
                    {
                        Id = (Guid)osobe[i]["Id"],
                        Ime = (string)osobe[i]["Ime"],
                        Prezime = (string)osobe[i]["Prezime"],
                        DatumRodenja = formatted,
                        Spol = (int)osobe[i]["Spol"],
                        Telefon = (string)osobe[i]["Telefon"],
                        Adresa = (string)osobe[i]["Adresa"]
                    });
                }
            }

            return popisOsoba;
        }


        // POST: api/Osoba
        [HttpPost]
        public Osoba Post([FromBody] Osoba osoba)
        {
            var nodeId = 0;
            StreamReader streamReader = new StreamReader(_path);
            string json = "";
            using (streamReader)
            {
                json = streamReader.ReadToEnd();
            }
            JObject jsonObject = JObject.Parse(json);
            var osobe = jsonObject["osoba"].ToList();
            List<Osoba> popisOsoba = new List<Osoba>();

            if(osobe.Count() == 0)
            {
                popisOsoba.Add(osoba);
                nodeId = osobe.Count();
                jsonObject["osoba"] =JToken.FromObject(popisOsoba);
            }
            else
            {
                jsonObject["osoba"].Last.AddAfterSelf(JToken.FromObject(osoba));

            }
 
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            File.WriteAllText(_path, output);

            return osoba;
        }

        
        // PUT: api/Osoba/5
        public Osoba Put(Guid id, [FromBody] Osoba osoba)
        {
            var nodeId = 0;
            StreamReader streamReader = new StreamReader(_path);
            string json = "";
            using (streamReader)
            {
                json = streamReader.ReadToEnd();
            }
            JObject jsonObject = JObject.Parse(json);
            var osobe = jsonObject["osoba"].ToList();
            List<Osoba> popisOsoba = new List<Osoba>();
            for (int i = 0; i < osobe.Count(); i++)
            {
                if ((Guid)osobe[i]["Id"] == id)
                {
                    nodeId = i;
                }
            }

            jsonObject["osoba"][nodeId]["Ime"] = osoba.Ime;
            jsonObject["osoba"][nodeId]["Prezime"] = osoba.Prezime;
            jsonObject["osoba"][nodeId]["DatumRodenja"] = osoba.DatumRodenja;
            jsonObject["osoba"][nodeId]["Spol"] = osoba.Spol;
            jsonObject["osoba"][nodeId]["Telefon"] = osoba.Telefon;
            jsonObject["osoba"][nodeId]["Adresa"] = osoba.Adresa;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_path, output);

            return osoba;
        }


        // DELETE: api/Osoba/5
        public int Delete(Guid id)
        {
            var nodeId= 0;
            StreamReader streamReader = new StreamReader(_path);
            string json = "";
            using (streamReader)
            {
                json = streamReader.ReadToEnd();
            }
            JObject jsonObject = JObject.Parse(json);
            var osobe = jsonObject["osoba"].ToList();
            List<Osoba> popisOsoba = new List<Osoba>();
            for (int i = 0; i < osobe.Count(); i++)
            {
                if ((Guid)osobe[i]["Id"] == id)
                {
                    nodeId = i;
                }
            }

            jsonObject["osoba"][nodeId].Remove();
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_path, output);

            return nodeId;
        }
    }
}
