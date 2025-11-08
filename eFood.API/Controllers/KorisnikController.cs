using eFood.Model;
using eFood.Model.Requests;
using eFood.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Korisnik")]
    [AllowAnonymous]
    public class KorisnikController : BaseCRUDController<Model.Korisnik, KorisnikSearchRequests, KorisnikInsertRequest, KorisnikUpsertRequest>
    {
        public KorisnikController(ILogger<BaseController<Model.Korisnik, KorisnikSearchRequests>> logger, IKorisniciService service) : base(logger, service)
        {
        }
        [HttpPost]
        [Authorize(Roles = "Korisnik")]
        public override async Task<Korisnik> Insert([FromBody] KorisnikInsertRequest insert)
        {
            return await base.Insert(insert);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public Task<Model.Korisnik> Login(string username, string password)
        {
            return (_service as IKorisniciService).Login(username, password);
        }
        [HttpGet("Authenticate")]
        [AllowAnonymous]

        public Task<Model.Korisnik> Authenticate()
        {
            string authorization = HttpContext.Request.Headers["Authorization"];

            string encodedHeader = authorization["Basic ".Length..].Trim();

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedHeader));

            int seperatorIndex = usernamePassword.IndexOf(':');

            return ((IKorisniciService)_service).Login(usernamePassword.Substring(0, seperatorIndex), usernamePassword[(seperatorIndex + 1)..]);
        }
        [HttpPost("registration")]
        [AllowAnonymous]
        public Task<Model.Korisnik> Register(string username, string password, string ime, string prezime)
        {
            return (_service as IKorisniciService).Register(username, password, ime, prezime);
        }


        /*[AllowAnonymous] [Authorize(Roles = "Administrator")] 
        public Task<Korisnik> Insert([FromBody] KorisnikUpsertRequest insert)
        {
            return base.Insert(insert);
        }*/

        /* [AllowAnonymous]
         public Task<PagedResult<Korisnik>> Get([FromQuery] KorisnikSearchRequests? search)
         {
             return base.Get(search);
         }

         [AllowAnonymous]
         public Task<Korisnik> GetById(int id)
         {
             return base.GetById(id);
         }*/

    }
}
