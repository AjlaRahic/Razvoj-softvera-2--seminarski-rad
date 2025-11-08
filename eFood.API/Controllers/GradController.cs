using eFood.Model;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    public class GradController : BaseController<Model.Grad, GradSearchObject>
    {
        public GradController(ILogger<BaseController<Grad, GradSearchObject>> logger, IGradService service) : base(logger, service)
        {
        }
    }
}
