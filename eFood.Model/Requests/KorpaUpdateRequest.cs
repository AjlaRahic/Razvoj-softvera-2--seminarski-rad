using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class KorpaUpdateRequest
    {
        // public int KorpaId { get; set; }
        //public int ProizvodId { get; set; }
        // public int KorisnikId { get; set; }
        public decimal? Cijena { get; set; }
        //public int? KategorijaId { get; set; }
        public int? Kolicina { get; set; }
    }
}
