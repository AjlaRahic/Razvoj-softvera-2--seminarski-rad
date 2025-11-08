using AutoMapper;
using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    public class KorpaController : BaseCRUDController<Model.Korpa, KorpaSearchObject, KorpaInsertRequest, KorpaUpdateRequest>
    {

        private readonly IMapper _mapper;
        public KorpaController(ILogger<BaseController<Model.Korpa, KorpaSearchObject>> logger, IKorpaService service, IMapper mapper) : base(logger, service)
        {
            _mapper = mapper;
        }
    }
}
