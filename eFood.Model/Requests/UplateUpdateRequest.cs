using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class UplateUpdateRequest
    {
        //  public int Id { get; set; }
        public double Iznos { get; set; }
        public string BrojTransakcije { get; set; }
        public DateTime DatumTransakcije { get; set; }
        public int KorisnikId { get; set; }
    }
}
