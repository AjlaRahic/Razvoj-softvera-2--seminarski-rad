using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class Uplata
    {
        public int Id { get; set; }

        public decimal? Iznos { get; set; }

        public DateTime? DatumTransakcije { get; set; }

        public string? BrojTransakcije { get; set; }

        public int? KorisnikId { get; set; }

        public virtual Korisnici? Korisnik { get; set; }
        public string? NacinPlacanja { get; set; }
    }
}
