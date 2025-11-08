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
    public class RestoranService : BaseCRUDService<Model.Restoran, Database.Restoran, BaseSearchObject, RestoranInsertRequest, RestoranUpdateRequest>, IRestoranService
    {
        public RestoranService(EFoodContext context, IMapper mapper)
            : base(context, mapper)
        {

        }


    }
}
