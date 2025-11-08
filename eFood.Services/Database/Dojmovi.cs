using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class Dojmovi
    {
        public int Id { get; set; }

        public int? Ocjena { get; set; }

        public string? Opis { get; set; }
        // public DateTime? DatumRecenzije { get; set; }

        public int? JeloId { get; set; }

        public int? KorisnikId { get; set; }

        public virtual Jelo? Jelo { get; set; }

        public virtual Korisnici? Korisnik { get; set; }
    }
}
