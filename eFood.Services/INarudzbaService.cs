using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public interface INarudzbaService : ICRUDService<Model.Narudzba, NarudzbaSearchObject, NarudzbaInsertRequest, NarudzbaUpdateRequest>
    {
        Task<Model.Narudzba> Activate(int id);

        Task<Model.Narudzba> Hide(int id);

        Task<List<string>> AllowedActions(int id);
        Task<Model.Narudzba> Checkout(NarudzbaCheckoutRequest request);
        Task<int> CheckoutFromCart(int korisnikId, int? statusId = null, string? paymentId = null, DateTime? datumNarudzbe = null);

    }
}
