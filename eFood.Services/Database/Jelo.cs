using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class Jelo
    {
        public int JeloId { get; set; }

        public string? Naziv { get; set; }

        public string? Opis { get; set; }

        public decimal? Cijena { get; set; }

        public int? KategorijaId { get; set; }

        public byte[]? Slika { get; set; }
        // public byte[]? SlikaThumb { get; set; }
        public string? StateMachine { get; set; }

        public virtual ICollection<Dojmovi> Dojmovis { get; set; } = new List<Dojmovi>();

        public virtual Kategorija? Kategorija { get; set; }

        public virtual ICollection<StavkeNarudzbe> StavkeNarudzbes { get; set; } = new List<StavkeNarudzbe>();
    }
}
