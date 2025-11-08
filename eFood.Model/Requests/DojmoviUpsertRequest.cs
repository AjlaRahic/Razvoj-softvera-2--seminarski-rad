using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace eFood.Model.Requests
{
    public class DojmoviUpsertRequest
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Ocjena { get; set; }
        public string Opis { get; set; }
        public DateTime? DatumRecenzije { get; set; }
        public int JeloId { get; set; }
        public int KorisnikId { get; set; }
    }
}
