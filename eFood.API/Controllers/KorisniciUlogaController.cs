using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    public class KorisniciUlogaController : BaseCRUDController<Model.KorisniciUloge, KorisniciUlogaSearchRequest, KorisniciUlogeInsertRequest, KorisniciUlogeUpdateRequest>
    {
        public KorisniciUlogaController(ILogger<BaseController<Model.KorisniciUloge, KorisniciUlogaSearchRequest>> logger, IKorisniciUloga service) : base(logger, service)
        {
        }
    }
}
