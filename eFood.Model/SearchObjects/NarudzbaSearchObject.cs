using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.SearchObjects
{
    public class NarudzbaSearchObject : BaseSearchObject
    {
        public int Id { get; set; }
        public DateTime? DatumNarudzbe { get; set; }
        public int StatusNarudzbeId { get; set; }
        public int KorisnikId { get; set; }
    }
}
