using AspWebApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspWebApp.Controllers
{
    public class SpolController : ApiController
    {
        private readonly string _path = $"C:\\Users\\Silvio\\Source\\repos\\AspWebApp\\assets\\lib\\Osoba.json";


        // GET: api/Spol
        public List<Spol> Get()
        {
            StreamReader streamReader = new StreamReader(_path);
            string json = "";
            using (streamReader)
            {
                json = streamReader.ReadToEnd();
            }

            JObject jsonObject = JObject.Parse(json);

            var spolovi = jsonObject["spol"].ToList();

            List<Spol> popisSpolova= new List<Spol>();
            for (int i = 0; i < spolovi.Count(); i++)
            {
                popisSpolova.Add(new Spol()
                {
                    Id = (int)spolovi[i]["Id"],
                    Naziv= (string)spolovi[i]["Naziv"],
                });
            }

            return popisSpolova;
        }


        // GET: api/Spol/5
        public List<Spol> Get(int id)
        {
            StreamReader streamReader = new StreamReader(_path);
            string json = "";
            using (streamReader)
            {
                json = streamReader.ReadToEnd();
            }
            JObject jsonObject = JObject.Parse(json);
            var spolovi= jsonObject["spol"].ToList();
            List<Spol> popisSpolova= new List<Spol>();

            for (int i = 0; i < spolovi.Count(); i++)
            {
                if ((int)spolovi[i]["Id"] == id)
                {
                    popisSpolova.Add(new Spol()
                    {
                        Id = (int)spolovi[i]["Id"],
                        Naziv = (string)spolovi[i]["Naziv"]
                    });
                }
            }

            return popisSpolova;
        }


        // POST: api/Spol
        public void Post([FromBody]string value)
        {
        }


        // PUT: api/Spol/5
        public void Put(int id, [FromBody]string value)
        {
        }


        // DELETE: api/Spol/5
        public void Delete(int id)
        {
        }
    }
}
