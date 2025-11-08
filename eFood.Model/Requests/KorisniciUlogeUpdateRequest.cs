using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class KorisniciUlogeUpdateRequest
    {
        // public int? KorisniciUlogaId { get; set; }
        public int? UlogaId { get; set; }
        public int? KorisnikId { get; set; }
        public DateTime? DatumIzmjene { get; set; }
    }
}
