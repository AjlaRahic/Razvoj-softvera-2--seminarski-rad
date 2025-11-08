using AutoMapper;
using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public class StavkeNarudzbeService : BaseCRUDService<Model.StavkeNarudzbe, Database.StavkeNarudzbe, StavkeNarudzbeSearchObject, StavkeNarudzbeInsertRequest, StavkeNarudzbeUpdateRequest>, IStavkeNarudzbeService
    {
        public StavkeNarudzbeService(EFoodContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /* [HttpPost]
         public async Task<List<Model.StavkeNarudzbe>> InsertAsync(List<StavkeNarudzbeUpsertRequest> request)
         {
             var entities = request.Select(i => _mapper.Map<StavkeNarudzbe>(i)).ToList();



             await _context.StavkeNarudzbes.AddRangeAsync(entities);
             await _context.SaveChangesAsync();



             var model = entities.Select(i => _mapper.Map<Model.StavkeNarudzbe>(i)).ToList();
             return model;
         }*/
        public override IQueryable<StavkeNarudzbe> AddFilter(IQueryable<StavkeNarudzbe> query, StavkeNarudzbeSearchObject? search = null)
        {
            var filter = base.AddFilter(query, search);

            if (search?.JeloId != null && search.JeloId > 0)
            {
                filter = filter.Where(x => x.JeloId == search.JeloId);
            }
            if (search?.NarudzbaId != null && search.NarudzbaId > 0)
            {
                filter = filter.Where(x => x.NarudzbaId == search.NarudzbaId);
            }
            return filter;
        }
    }
}
