using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.SearchObjects
{
    public class KorisniciUlogaSearchRequest : BaseSearchObject
    {
        public int? KorisnikId { get; set; }
        public int? UlogaId { get; set; }
    }
}
