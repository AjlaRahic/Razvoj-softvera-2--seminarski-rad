using eFood.Model.Requests;
using eFood.Services;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    // [AllowAnonymous]
    public class KategorijaController : BaseCRUDController<Model.Kategorija, KategorijaSearchRequest, KategorijaInsertRequest, KategorijaUpdateRequest>
    {
        public KategorijaController(ILogger<BaseController<Model.Kategorija, KategorijaSearchRequest>> logger, IKategorijaService service) : base(logger, service)
        {
        }
        public override Task<Model.Kategorija> Insert([FromBody] KategorijaInsertRequest insert)
        {
            return base.Insert(insert);
        }
    }
}
