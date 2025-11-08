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
    public class DojmoviService : BaseCRUDService<Model.Dojmovi, Database.Dojmovi, DojmoviSearchObject, DojmoviInsertRequest, DojmoviUpsertRequest>, IDojmoviService
    {
        public DojmoviService(EFoodContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }
}
