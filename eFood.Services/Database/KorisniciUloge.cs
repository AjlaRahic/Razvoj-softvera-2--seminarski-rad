using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class KorisniciUloge
    {
        public int? KorisnikUlogaId { get; set; }
        public int? KorisnikId { get; set; } // Make this non-nullable

        public int? UlogaId { get; set; } // Make this non-nullable

        public DateTime? DatumIzmjene { get; set; }

        public virtual Korisnici? Korisnik { get; set; } // No need for nullable reference type
        public virtual Uloge? Uloga { get; set; } // No need for nullable reference type
    }
}
