using AutoMapper;
using eFood.Model.SearchObjects;
using eFood.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public class UlogaService : BaseService<Model.Uloge, Database.Uloge, UlogeSearchObject>, IUlogaService
    {
        public UlogaService(EFoodContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Uloge> AddFilter(IQueryable<Uloge> query, UlogeSearchObject search = null)
        {
            var filter = base.AddFilter(query, search);
            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                filter = filter.Where(w => w.Naziv.Contains(search.Naziv));
            }
            return filter;
        }
    }
}
