using AutoMapper;
using eFood.Model;
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
    public class JeloService : BaseCRUDService<Model.Jelo, Database.Jelo, JeloSearchObject, JeloUpsertRequest, JeloUpsertRequest>, IJeloService
    {
        public JeloService(EFoodContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*public IActionResult UploadImage([FromBody] ImageUploadRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Image))
            {
                return new BadRequestObjectResult("No image provided");
            }

            try
            {
                byte[] imageBytes = Convert.FromBase64String(request.Image);
                string filePath = Path.Combine("wwwroot/images", $"{Guid.NewGuid()}.jpg"); // Generiši jedinstveno ime fajla
                System.IO.File.WriteAllBytes(filePath, imageBytes);
                return new OkObjectResult("Image uploaded successfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error uploading image: {ex.Message}");
            }
        }*/

        public List<Model.Jelo> GetPreporucenaJela(int trenutniKorisnikId)
        {
            var korisnici = _context.Korisnicis.ToList();

            Dictionary<Database.Korisnici, List<Database.Dojmovi>> dojmovi = new Dictionary<Database.Korisnici, List<Database.Dojmovi>>();
            foreach (var korisnik in korisnici)
            {
                var ocjene = _context.Dojmovis
                    .Where(e => e.KorisnikId == korisnik.Id)
                    .ToList();
                dojmovi.Add(korisnik, ocjene);
            }

            var razloziPoJelu = new Dictionary<int, string>();
            var bestSimPoJelu = new Dictionary<int, double>();

            var ja = dojmovi.FirstOrDefault(x => x.Key.Id == trenutniKorisnikId).Value ?? new List<Database.Dojmovi>();

            var zajednickeOcjeneKorisnik = new List<Database.Dojmovi>();
            var zajednickeOcjeneKorisnik2 = new List<Database.Dojmovi>();
            var preporucenaJelaIds = new HashSet<int>();

            foreach (var korisnik1 in dojmovi)
            {
                if (korisnik1.Key.Id == trenutniKorisnikId) continue;

                foreach (var korisnik2 in dojmovi)
                {
                    if (korisnik1.Key.Id == korisnik2.Key.Id) continue;

                    foreach (var ocjena1 in korisnik1.Value)
                    {
                        if (korisnik2.Value.Any(x => x.JeloId == ocjena1.JeloId))
                        {
                            zajednickeOcjeneKorisnik.Add(ocjena1);
                            zajednickeOcjeneKorisnik2.Add(korisnik2.Value.FirstOrDefault(x => x.JeloId == ocjena1.JeloId));
                        }
                    }

                    double slicnost = GetSlicnost(zajednickeOcjeneKorisnik, zajednickeOcjeneKorisnik2);
                    if (slicnost > 0.5)
                    {
                        var dobroOcjenjenaJelaIds = korisnik2.Value
                            .Where(e => e.Ocjena >= 3)
                            .Select(e => e.JeloId)
                            .ToList();

                        foreach (var jeloIdObj in dobroOcjenjenaJelaIds)
                        {
                            var jeloId = (int)jeloIdObj;
                            preporucenaJelaIds.Add(jeloId);

                            var simSaMnom = CosineFromLists(ja, korisnik2.Value);
                            var imeSlicnog = korisnik2.Key.Ime ?? $"Korisnik #{korisnik2.Key.Id}";
                            var reason = simSaMnom > 0
                                ? $"Sličan korisnik: {imeSlicnog} (sim. {simSaMnom:F2})"
                                : "Popularno među korisnicima u ovom periodu";

                            if (!bestSimPoJelu.TryGetValue(jeloId, out var best) || simSaMnom > best)
                            {
                                bestSimPoJelu[jeloId] = simSaMnom;
                                razloziPoJelu[jeloId] = reason;
                            }
                        }
                    }

                    zajednickeOcjeneKorisnik.Clear();
                    zajednickeOcjeneKorisnik2.Clear();
                }
            }

            var preporucenaJela = _context.Set<Database.Jelo>()
                .Where(x => preporucenaJelaIds.Contains(x.JeloId))
                .ToList();

            var result = _mapper.Map<List<Model.Jelo>>(preporucenaJela);

            foreach (var j in result)
            {
                var idProp = j.GetType().GetProperty("JeloId");
                if (idProp == null) continue;

                var idVal = idProp.GetValue(j);
                if (idVal is int jid && razloziPoJelu.TryGetValue(jid, out var reasonText))
                {
                    TryAttachReasonToJelo(j, reasonText);
                }
            }

            return result;
        }

        // Kosinus između dvije liste ocjena (po JeloId), za potrebe "razloga" (precizan double)
        private double CosineFromLists(List<Database.Dojmovi> a, List<Database.Dojmovi> b)
        {
            if (a == null || b == null || a.Count == 0 || b.Count == 0) return 0;

            var da = a.Where(x => x.JeloId.HasValue && x.Ocjena.HasValue)
                      .ToDictionary(x => x.JeloId!.Value, x => (double)x.Ocjena!.Value);
            var db = b.Where(x => x.JeloId.HasValue && x.Ocjena.HasValue)
                      .ToDictionary(x => x.JeloId!.Value, x => (double)x.Ocjena!.Value);

            var common = da.Keys.Intersect(db.Keys).ToList();
            if (common.Count == 0) return 0;

            double dot = 0, na = 0, nb = 0;
            foreach (var k in common)
            {
                var va = da[k];
                var vb = db[k];
                dot += va * vb;
            }
            foreach (var v in da.Values) na += v * v;
            foreach (var v in db.Values) nb += v * v;

            if (na == 0 || nb == 0) return 0;
            return dot / (Math.Sqrt(na) * Math.Sqrt(nb));
        }

        // Pokuša smjestiti reason u postojeće string polje modela; fallback: dodaj u Naziv
        private void TryAttachReasonToJelo(Model.Jelo j, string reason)
        {
            var t = j.GetType();

            // 1) pokušaj tipične string-properije za reason
            string[] candidates = { "ReasonText", "Razlog", "OpisPreporuke", "Opis", "Napomena" };
            foreach (var name in candidates)
            {
                var p = t.GetProperty(name);
                if (p != null && p.CanWrite && p.PropertyType == typeof(string))
                {
                    p.SetValue(j, reason);
                    return;
                }
            }

            // 2) fallback: dodaj na kraj Naziv, ako je settable string
            var nazivProp = t.GetProperty("Naziv");
            if (nazivProp != null && nazivProp.CanWrite && nazivProp.PropertyType == typeof(string))
            {
                var old = nazivProp.GetValue(j) as string ?? "";
                // izbjegni dupliranje ako već sadrži reason
                if (!old.Contains(reason))
                {
                    nazivProp.SetValue(j, string.IsNullOrWhiteSpace(old) ? reason : $"{old} • {reason}");
                }
            }
        }




        private double GetSlicnost(List<Database.Dojmovi> zajednickeOcjene1, List<Database.Dojmovi> zajednickeOcjene2)
        {
            if (zajednickeOcjene1.Count != zajednickeOcjene2.Count)
                return 0;

            int? brojnik = 0, nazivnik1 = 0, nazivnik2 = 0;
            for (int i = 0; i < zajednickeOcjene1.Count; i++)
            {
                brojnik += zajednickeOcjene1[i].Ocjena * zajednickeOcjene2[i].Ocjena;
                nazivnik1 += zajednickeOcjene1[i].Ocjena * zajednickeOcjene1[i].Ocjena;
                nazivnik2 += zajednickeOcjene2[i].Ocjena * zajednickeOcjene2[i].Ocjena;
            }
            nazivnik1 = (int?)Math.Sqrt((double)nazivnik1);
            nazivnik2 = (int?)Math.Sqrt((double)nazivnik2);
            int? nazivnik = nazivnik1 * nazivnik2;
            if (nazivnik == 0)
                return 0;
            else
                return (double)(brojnik / nazivnik);
        }

        public override IQueryable<eFood.Services.Database.Jelo> AddFilter(IQueryable<eFood.Services.Database.Jelo> query, JeloSearchObject? search = null)

        {
            var filter = base.AddFilter(query, search);

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                filter = filter.Where(w => w.Naziv.ToLower().Contains(search.Naziv.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.KategorijaNaziv))
            {
                filter = filter.Where(x => x.Kategorija.Naziv == search.KategorijaNaziv);
            }
            if (search?.KategorijaId != null && search.KategorijaId > 0)
            {
                filter = filter.Where(x => x.KategorijaId == search.KategorijaId);
            }
            return filter;
        }

    }

}
