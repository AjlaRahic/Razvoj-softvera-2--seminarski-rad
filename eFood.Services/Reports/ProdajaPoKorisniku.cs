using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Reports
{
    public class ProdajaPoKorisniku
    {
        public int? KorisnikId { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public decimal? UkupniIznos { get; set; }
    }
}
