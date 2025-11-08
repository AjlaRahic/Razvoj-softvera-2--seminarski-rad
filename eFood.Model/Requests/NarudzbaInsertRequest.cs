using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class NarudzbaInsertRequest
    {
        // public int Id { get; set; }
        public DateTime DatumNarudzbe { get; set; }
        public int KorisnikId { get; set; }
        // public string Korisnik { get; set; }
        public int? StatusNarudzbeId { get; set; }
        public string StatusNarudzbe { get; set; }
        public string? StateMachine { get; set; }
    }
}
