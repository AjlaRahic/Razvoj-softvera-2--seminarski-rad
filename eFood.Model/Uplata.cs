using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model
{
    public class Uplata
    {
        public int Id { get; set; }
        public double Iznos { get; set; }
        public string BrojTransakcije { get; set; }
        public DateTime DatumTransakcije { get; set; }
        public int KorisnikId { get; set; }
        public string NacinPlacanja { get; set; }
    }
}
