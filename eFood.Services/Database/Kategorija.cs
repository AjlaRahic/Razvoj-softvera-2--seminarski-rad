using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class Kategorija
    {
        public int KategorijaId { get; set; }

        public string? Naziv { get; set; }

        public string? Opis { get; set; }

        public virtual ICollection<Jelo> Jelos { get; set; } = new List<Jelo>();
    }
}
