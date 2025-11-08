using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public class Restoran
    {
        public int RestoranId { get; set; }
        public string? NazivRestorana { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        public string? Adresa { get; set; }
        public int? GradId { get; set; }
        public virtual Grad? Grad { get; set; }
    }
}
