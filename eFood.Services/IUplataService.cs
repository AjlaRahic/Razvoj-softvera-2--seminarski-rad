using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public interface IUplataService : ICRUDService<Model.Uplata, UplataSearchObject, UplataInsertRequest, UplateUpdateRequest>
    {
    }
}
