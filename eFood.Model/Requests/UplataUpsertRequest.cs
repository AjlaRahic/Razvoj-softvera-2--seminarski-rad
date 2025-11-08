using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eFood.Model.Requests
{
    public class UplataUpsertRequest
    {
        [Range(0, double.MaxValue)]
        public double Iznos { get; set; }
        public string DatumTransakcije { get; set; }
        public string BrojTransakcije { get; set; }
        public int KorisnikId { get; set; }
    }
}
