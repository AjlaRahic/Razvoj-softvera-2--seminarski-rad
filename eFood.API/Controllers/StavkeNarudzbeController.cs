using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{

    [Route("[controller]")]
    // [AllowAnonymous]
    public class StavkeNarudzbeController : BaseCRUDController<Model.StavkeNarudzbe, StavkeNarudzbeSearchObject, StavkeNarudzbeInsertRequest, StavkeNarudzbeUpdateRequest>
    {
        public StavkeNarudzbeController(ILogger<BaseController<Model.StavkeNarudzbe, StavkeNarudzbeSearchObject>> logger, IStavkeNarudzbeService service) : base(logger, service)
        {
        }
    }
}
