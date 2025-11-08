using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class Uloge
    {
        public int Id { get; set; }

        public string? Naziv { get; set; }

        public string? Opis { get; set; }

        public virtual ICollection<KorisniciUloge> KorisniciUloges { get; set; } = new List<KorisniciUloge>();
    }
}
