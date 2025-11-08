using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class Korisnici
    {
        public Korisnici()
        {
            KorisniciUloges = new HashSet<KorisniciUloge>();
        }
        public int Id { get; set; }

        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        public string? KorisnickoIme { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }

        public string? LozinkaHash { get; set; }

        public string? LozinkaSalt { get; set; }

        public virtual ICollection<Dojmovi> Dojmovis { get; set; } = new List<Dojmovi>();

        public virtual ICollection<KorisniciUloge> KorisniciUloges { get; set; } = new List<KorisniciUloge>();

        public virtual ICollection<Narudzba> Narudzbas { get; set; } = new List<Narudzba>();

        public virtual ICollection<Uplata> Uplata { get; set; } = new List<Uplata>();
    }
}
