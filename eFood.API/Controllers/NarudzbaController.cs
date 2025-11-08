using eFood.Model;
using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace eFood.API.Controllers
{
    [Route("[controller]")]
    public class NarudzbaController : BaseCRUDController<Model.Narudzba, NarudzbaSearchObject, NarudzbaInsertRequest, NarudzbaUpdateRequest>
    {
        protected readonly INarudzbaService _service;
        private readonly IMailProducer _mailProducer;
        public NarudzbaController(IMailProducer mailProducer, ILogger<BaseController<Model.Narudzba, NarudzbaSearchObject>> logger, INarudzbaService service)
         : base(logger, service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mailProducer = mailProducer;
        }

        public class EmailModel
        {
            public string Sender { get; set; }
            public string Recipient { get; set; }
            public string Subject { get; set; }
            public string Content { get; set; }
        }



        [HttpPost("checkout")]
        public async Task<ActionResult<int>> Checkout([FromBody] NarudzbaCheckoutRequest request)
        {
            var narudzba = await _service.Checkout(request);
            return Ok(narudzba.Id);

        }

        [HttpPost("checkoutFromCart")]
        //[HttpPost("checkout-from-cart")]
        public async Task<ActionResult<int>> CheckoutFromCart([FromBody] CheckoutFromCartRequest req)
        {
            if (req == null || req.KorisnikId <= 0)
                return BadRequest("Neispravan zahtjev.");

            var id = await _service.CheckoutFromCart(req.KorisnikId, req.StatusId, req.PaymentId, req.DatumNarudzbe);
            return Ok(id);
        }



        [HttpPut("{id}/activate")]
        public virtual async Task<Narudzba> Activate(int id)
        {
            return await _service.Activate(id);
        }

        [HttpPut("{id}/hide")]
        public virtual async Task<Narudzba> Hide(int id)
        {
            return await _service.Hide(id);
        }

        [HttpGet("{id}/allowedActions")]
        public virtual async Task<List<string>> AllowedActions(int id)
        {
            return await _service.AllowedActions(id);
        }
        /*
        [HttpPut("{id}/activate")]
        public virtual async Task<Narudzba> Activate(int id)
        {
            return await (_service as INarudzbaService).Activate(id);
        }


        [HttpPut("{id}/hide")]
        public virtual async Task<Narudzba> Hide(int id)
        {
            return await (_service as INarudzbaService).Hide(id);
        }

        [HttpGet("{id}/allowedActions")]
        public virtual async Task<List<string>> AllowedActions(int id)
        {
            return await (_service as INarudzbaService).AllowedActions(id);
        }


        /*  [HttpGet("{id}/allowedActions")]
          public virtual async Task<List<string>> AllowedActions(int id)
          {
              return await _service.AllowedActions(id);
          }
        */
        /*

          [HttpPut("{id}/accept")]
          public virtual async Task<Narudzba> Accept(int id)
          {
              return await _service.Accept(id);
          }

          [HttpPut("{id}/inProgress")]
          public virtual async Task<Narudzba> inProgress(int id)
          {
              return await _service.InProgress(id);
          }

          [HttpPut("{id}/finish")]
          public virtual async Task<Narudzba> Finish(int id)
          {
              return await _service.Finish(id);
          }

          [HttpPut("{id}/deliver")]
          public virtual async Task<Narudzba> Deliver(int id)
          {
              return await _service.Deliver(id);
          }

          [HttpPut("{id}/cancel")]
          public virtual async Task<Narudzba> Cancel(int id)
          {
              return await _service.Cancel(id);
          }*/
    }
}
