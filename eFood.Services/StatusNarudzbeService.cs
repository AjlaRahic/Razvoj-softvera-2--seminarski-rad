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
    public class StatusNarudzbeService : BaseService<Model.StatusNarudzbe, Database.Status, StatusNarudzbeSearchObject>, IStatusNarudzbeService
    {
        public StatusNarudzbeService(EFoodContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public override IQueryable<Database.Status> AddFilter(IQueryable<Database.Status> query, StatusNarudzbeSearchObject? search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                filteredQuery = filteredQuery.Where(x => x.Naziv.Contains(search.Naziv.ToLower()));
            }
            return filteredQuery;
        }

    }
}
