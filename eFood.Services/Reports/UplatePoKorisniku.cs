using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Reports
{
    public class UplatePoKorisniku
    {
        public decimal? Iznos { get; set; }
        public DateTime? DatumTransakcije { get; set; }
        public string? BrojTransakcije { get; set; }
        public string? ImeKorisnika { get; set; }
        public string? PrezimeKorisnika { get; set; }
        public string? NacinPlacanja { get; set; }
    }
}
