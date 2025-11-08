using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eFood.Model.Requests
{
    public class JeloUpsertRequest
    {
        public string? Naziv { get; set; }
        public string? Opis { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Cijena { get; set; }
        public byte[]? Slika { get; set; }
        public int? KategorijaId { get; set; }
    }
}
