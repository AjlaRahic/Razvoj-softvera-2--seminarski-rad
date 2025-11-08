using eFood.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public interface IKategorijaService : ICRUDService<Model.Kategorija, KategorijaSearchRequest, KategorijaInsertRequest, KategorijaUpdateRequest>
    {
    }
}
