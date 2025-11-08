using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Database
{
    public partial class Status
    {
        public int Id { get; set; }

        public string? Naziv { get; set; }

        public virtual ICollection<Narudzba> Narudzbas { get; set; } = new List<Narudzba>();
    }
}
