using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    //[AllowAnonymous]
    public class UplataController : BaseCRUDController<Model.Uplata, UplataSearchObject, UplataInsertRequest, UplateUpdateRequest>
    {
        public UplataController(ILogger<BaseController<Model.Uplata, UplataSearchObject>> logger, IUplataService service) : base(logger, service)
        {
        }
    }
}
