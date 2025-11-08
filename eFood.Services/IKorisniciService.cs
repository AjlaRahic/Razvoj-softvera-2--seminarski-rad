using eFood.Model;
using eFood.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public interface IKorisniciService : ICRUDService<Korisnik, KorisnikSearchRequests, KorisnikInsertRequest, KorisnikUpsertRequest>
    {
        Task<Model.Korisnik> InsertAsync(KorisnikUpsertRequest korisnici);
        Task<Model.Korisnik> Login(string username, string password);
        Task<Model.Korisnik> Register(string username, string password, string ime, string prezime);
        Task<Korisnik> Insert(KorisnikInsertRequest insert);


    }
}
