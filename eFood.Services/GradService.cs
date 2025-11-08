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
    public class GradService : BaseService<Model.Grad, Database.Grad, GradSearchObject>, IGradService
    {
        public GradService(EFoodContext context, IMapper mapper)
            : base(context, mapper)
        {

        }
        public override IQueryable<Database.Grad> AddFilter(IQueryable<Database.Grad> query, GradSearchObject? search = null)
        {
            var filteredQuery = base.AddFilter(query, search);
            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                query = query.Where(x => x.Naziv.Contains(search.Naziv.ToLower()));
            }
            return filteredQuery;
        }




    }
}
