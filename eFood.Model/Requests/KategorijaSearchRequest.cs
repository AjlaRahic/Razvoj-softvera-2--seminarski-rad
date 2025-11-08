using eFood.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class KategorijaSearchRequest : BaseSearchObject
    {
        public string Naziv { get; set; }
    }
}
