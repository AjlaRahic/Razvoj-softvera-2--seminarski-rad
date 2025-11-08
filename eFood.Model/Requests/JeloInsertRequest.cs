using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class JeloInsertRequest
    {
        // public int Id { get; set; }
        public string? Naziv { get; set; }
        public string? Opis { get; set; }
        public decimal? Cijena { get; set; }
        public int? KategorijaId { get; set; }
        // public Kategorija Kategorija { get; set; }
        public byte[]? Slika { get; set; }
        // public byte[]? SlikaThumb { get; set; }
    }
}
