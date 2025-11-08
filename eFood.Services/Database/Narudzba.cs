using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class Narudzba
    {
        public int Id { get; set; }

        public DateTime? DatumNarudzbe { get; set; }

        public int? KorisnikId { get; set; }

        public int? StatusNarudzbeId { get; set; }

        public virtual Korisnici? Korisnik { get; set; }

        public virtual Status? StatusNarudzbe { get; set; }

        public virtual ICollection<StavkeNarudzbe> StavkeNarudzbes { get; set; } = new List<StavkeNarudzbe>();
        public string? StateMachine { get; set; }
        public string? PaymentId { get; set; }
    }

}
