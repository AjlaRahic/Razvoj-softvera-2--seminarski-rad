using eFood.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class KorisnikSearchRequests : BaseSearchObject
    {
        public bool IsUlogeIncluded { get; set; }
    }
}
