using AutoMapper;
using eFood.Model.Requests;
using eFood.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public class KorpaService : BaseCRUDService<Model.Korpa, Database.Korpa, KorpaSearchObject, KorpaInsertRequest, KorpaUpdateRequest>, IKorpaService 
    {
        public KorpaService(EFoodContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IQueryable<Korpa> AddInclude(IQueryable<Korpa> query, KorpaSearchObject? search = null)
        {
            if (search?.IsJeloIncluded == true)
            {
                query = query.Include("Korpas.Jelo");
            }
            return base.AddInclude(query, search);
        }
    }
}
