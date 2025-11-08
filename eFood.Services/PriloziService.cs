using AutoMapper;
using eFood.Model.Requests;
using eFood.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public class PriloziService : BaseService<Model.Prilozi, Database.Prilozi, PriloziSearchObject>, IPriloziService
    {
        public PriloziService(EFoodContext context, IMapper mapper)
            : base(context, mapper)
        {

        }
        public override IQueryable<Database.Prilozi> AddFilter(IQueryable<Database.Prilozi> query, PriloziSearchObject? search = null)
        {
            var filteredQuery = base.AddFilter(query, search);
            if (!string.IsNullOrWhiteSpace(search?.NazivPriloga))
            {
                query = query.Where(x => x.NazivPriloga.Contains(search.NazivPriloga.ToLower()));
            }
            return filteredQuery;
        }

    }
}
