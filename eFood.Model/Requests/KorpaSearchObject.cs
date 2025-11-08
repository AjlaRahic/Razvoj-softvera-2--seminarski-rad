using eFood.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class KorpaSearchObject : BaseSearchObject
    {
        public bool IsJeloIncluded { get; set; }
    }
}
