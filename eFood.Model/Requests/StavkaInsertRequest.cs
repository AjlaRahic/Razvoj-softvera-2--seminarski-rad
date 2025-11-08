using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class StavkaInsertRequest
    {
        public int JeloId { get; set; }
        public int Kolicina { get; set; }
        public decimal Cijena { get; set; }
    }
}
