using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public class Korpa
    {
        public int KorpaId { get; set; }
        public int? JeloId { get; set; }
        public int? KorisnikId { get; set; }
        public decimal? Cijena { get; set; }
        public int? KategorijaId { get; set; }
        public int? Kolicina { get; set; }
        public int? PrilogId { get; set; }

        public virtual Jelo? Jelo { get; set; }
        public virtual Kategorija? Kategorija { get; set; }
        public virtual Prilozi? Prilozi { get; set; }



    }
}
