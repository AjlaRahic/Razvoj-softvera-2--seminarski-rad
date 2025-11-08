using eFood.Model;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    public class UlogaController : BaseController<Model.Uloge, UlogeSearchObject>
    {
        public UlogaController(ILogger<BaseController<Uloge, UlogeSearchObject>> logger, IUlogaService service) : base(logger, service)
        {
        }
    }
}
