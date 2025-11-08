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
    public class KorisniciUlogaService : BaseCRUDService<Model.KorisniciUloge, Database.KorisniciUloge, KorisniciUlogaSearchRequest, KorisniciUlogeInsertRequest, KorisniciUlogeUpdateRequest>, IKorisniciUloga
    {
        public KorisniciUlogaService(EFoodContext context, IMapper mapper) : base(context, mapper)
        {
            _context = _context;
            _mapper = _mapper;
        }

    }
}
