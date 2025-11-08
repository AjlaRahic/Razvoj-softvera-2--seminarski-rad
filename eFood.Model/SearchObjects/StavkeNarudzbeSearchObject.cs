using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.SearchObjects
{
    public class StavkeNarudzbeSearchObject : BaseSearchObject
    {
        public int JeloId { get; set; }
        public int NarudzbaId { get; set; }
        public int KorisnikId { get; set; }
    }
}
