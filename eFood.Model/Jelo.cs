using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model
{
    public class Jelo
    {
        public int JeloId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
        public int KategorijaId { get; set; }
        // public Kategorija Kategorija { get; set; }
        public byte[]? Slika { get; set; }
        public string? StateMachine { get; set; }

        //public byte[]? SlikaThumb { get; set; }
    
}
}
