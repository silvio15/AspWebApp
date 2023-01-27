using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace AspWebApp.Models
{
    public class OsobaModel
    {
        public Guid Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodenja { get; set; }
        public string Spol { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }

    }
}