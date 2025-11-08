using eFood.Model;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    public class DrzavaController : BaseController<Model.Drzava, DrzavaSearchObject>
    {
        public DrzavaController(ILogger<BaseController<Drzava, DrzavaSearchObject>> logger, IDrzavaService service) : base(logger, service)
        {
        }
    }
}
