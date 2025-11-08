using eFood.Model;
using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    public class RestoranController : BaseCRUDController<Model.Restoran, BaseSearchObject, RestoranInsertRequest, RestoranUpdateRequest>
    {
        public RestoranController(ILogger<BaseController<Restoran, BaseSearchObject>> logger, IRestoranService service) : base(logger, service)
        {
        }
    }
}
