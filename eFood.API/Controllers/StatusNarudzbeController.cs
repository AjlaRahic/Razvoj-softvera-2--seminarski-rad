using eFood.Model;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    public class StatusNarudzbeController : BaseController<Model.StatusNarudzbe, StatusNarudzbeSearchObject>
    {
        public StatusNarudzbeController(ILogger<BaseController<StatusNarudzbe, StatusNarudzbeSearchObject>> logger, IStatusNarudzbeService service) : base(logger, service)
        {
        }

    }
}
