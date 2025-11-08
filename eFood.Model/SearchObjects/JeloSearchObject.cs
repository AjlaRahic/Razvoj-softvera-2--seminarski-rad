using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.SearchObjects
{
    public class JeloSearchObject : BaseSearchObject
    {
        
        public string Naziv { get; set; }
        public string? KategorijaNaziv { get; set; }
        public int? KategorijaId { get; set; }

    }
}
