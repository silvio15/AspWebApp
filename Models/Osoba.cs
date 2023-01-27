using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspWebApp.Models
{
    public class Osoba
    {
        public Guid Id { get; set; }
        public string Ime { get; set; } 
        public string Prezime { get; set; } 
        public string DatumRodenja { get; set; }
        public int Spol{ get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }

    }
}