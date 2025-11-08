using AutoMapper;
using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public class UplataService : BaseCRUDService<Model.Uplata, Database.Uplata, UplataSearchObject, UplataInsertRequest, UplateUpdateRequest>, IUplataService
    {
        public UplataService(EFoodContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public override IQueryable<Database.Uplata> AddFilter(IQueryable<Database.Uplata> query, UplataSearchObject? search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            // Ako je search.KorisnikId int?, konvertuj ga u string?
            var korisnikIdString = search?.KorisnikId.ToString();

            if (!string.IsNullOrWhiteSpace(korisnikIdString))
            {
                filteredQuery = filteredQuery.Where(x => x.KorisnikId.ToString() == korisnikIdString);
            }
            return filteredQuery;
        }
        public async Task<Model.Uplata> InsertAsync(UplataUpsertRequest request)
        {
            var uplata = new Uplata()
            {
                Iznos = (decimal)request.Iznos,
                BrojTransakcije = request.BrojTransakcije,
                DatumTransakcije = DateTime.Parse(request.DatumTransakcije),
                KorisnikId = request.KorisnikId
            };

            await _context.Uplata.AddAsync(uplata);
            await _context.SaveChangesAsync();

            return _mapper.Map<Model.Uplata>(uplata);
        }
    }
}
