using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Reports
{
    public class Uplate
    {
        public int Id { get; set; }
        public decimal Iznos { get; set; }
        public DateTime DatumTransakcije { get; set; }
        public string? BrojTransakcije { get; set; }
    }
}
