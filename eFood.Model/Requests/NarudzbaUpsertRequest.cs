using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class NarudzbaUpsertRequest
    {
        public DateTime DatumNarudzbe { get; set; }
        public int KorisnikId { get; set; }
        public int StatusNarudzbeId { get; set; }
        public string? StateMachine { get; set; }
    }
}
