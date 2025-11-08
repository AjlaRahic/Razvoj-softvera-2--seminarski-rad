using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public interface IJeloService : ICRUDService<Model.Jelo, JeloSearchObject, JeloUpsertRequest, JeloUpsertRequest>
    {
        //List<Model.Jelo> GetPreporucenaJela(int korisnikId);
        List<Model.Jelo> GetPreporucenaJela(int korisnikId);

    }
}
