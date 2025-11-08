using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model
{
    public class Dojmovi
    {
        public int Id { get; set; }
        public int Ocjena { get; set; }
        public string Opis { get; set; }
        //  public DateTime? DatumRecenzije { get; set; }
        public int JeloId { get; set; }
        public int KorisnikId { get; set; }
    }
}
