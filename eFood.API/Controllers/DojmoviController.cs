using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    //[AllowAnonymous]
    public class DojmoviController : BaseCRUDController<Model.Dojmovi, DojmoviSearchObject, DojmoviInsertRequest, DojmoviUpsertRequest>
    {
        public DojmoviController(ILogger<BaseController<Model.Dojmovi, DojmoviSearchObject>> logger, IDojmoviService service) : base(logger, service)
        {
        }
    }
}
