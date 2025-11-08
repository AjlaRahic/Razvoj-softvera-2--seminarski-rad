using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class Drzava
    {
        public int Id { get; set; }

        public string? Naziv { get; set; }

        public virtual ICollection<Korisnici> Korisnicis { get; set; } = new List<Korisnici>();
    }
}
