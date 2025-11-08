using eFood.Model;
using eFood.Model.Requests;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    public class PriloziController : BaseController<Model.Prilozi, PriloziSearchObject>
    {
        public PriloziController(ILogger<BaseController<Prilozi, PriloziSearchObject>> logger, IPriloziService service) : base(logger, service)
        {
        }
    }
}
