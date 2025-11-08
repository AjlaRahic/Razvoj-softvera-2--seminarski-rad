using AutoMapper;
using eFood.Model.Requests;
using eFood.Model.SearchObjects;
using eFood.Services.Database;
using eFood.Services.NarudzbeStateMachine;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services
{
    public class NarudzbaService : BaseCRUDService<Model.Narudzba, Narudzba, NarudzbaSearchObject, NarudzbaInsertRequest, NarudzbaUpdateRequest>, INarudzbaService
    {
        public BaseNarudzbaState _baseState { get; set; }
// private readonly IMailProducer _mailProducer;
        public NarudzbaService(BaseNarudzbaState baseState, /*IMailProducer mailProducer,*/ EFoodContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _baseState = baseState;
            //_mailProducer = mailProducer;
        }

        public void BeforeInsert(NarudzbaInsertRequest insert, Database.Narudzba entity)
        {
            entity.KorisnikId = insert.KorisnikId;
            entity.StatusNarudzbeId = insert.StatusNarudzbeId;
            entity.DatumNarudzbe = insert.DatumNarudzbe;
            entity.StateMachine = insert.StateMachine;

            base.BeforeInsert(entity, insert);

           // SendEmailOnTerminInsert(entity.KorisnikId);
        }
        //private void SendEmailOnTerminInsert(int? korisnikId)
        //{
        //    var user = _context.Korisnicis.Find(korisnikId);
        //    if (user != null)
        //    {

        //        var emailMessage = new
        //        {
        //            Sender = "tt8915119@gmail.com",
        //            Recipient = user.Email,
        //            Subject = "Nova Naruzba je poslana",
        //            Content = $"Poštovani {user.Ime}, vasa narudzba je poslana."
        //        };

        //        _mailProducer.SendEmail(emailMessage);
        //    }
        //}

        public async Task AcceptServiceRequest(int narudzbaId)
        {
            try
            {
                var serviceRequest = await _context.Narudzbas.FindAsync(narudzbaId);
                if (serviceRequest == null)
                {
                    throw new InvalidOperationException("Narudzba request not found");
                }

                if (serviceRequest.DatumNarudzbe == null)
                {
                    throw new InvalidOperationException("Service request does not have valid date or time information");
                }

                if (serviceRequest.KorisnikId == null || serviceRequest.StatusNarudzbeId == null)
                {
                    throw new InvalidOperationException("PacijentId or DoktorId is null in the termin request.");
                }

                var user = await _context.Korisnicis.FindAsync(serviceRequest.KorisnikId);
                if (user != null)
                {
                    var emailMessage = new
                    {
                        Sender = "tt8915119@gmail.com",
                        Recipient = user.Email,
                        Subject = "Prihvaćena narudzba!",
                        Content = $"Zakazana narudzba za {serviceRequest.DatumNarudzbe} je prihvaćen od strane korisnika za pregled u vrijeme {serviceRequest.DatumNarudzbe}."
                    };

                   // _mailProducer.SendEmail(emailMessage);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while processing the request.", ex);
            }
        }
        public override async Task<Model.Narudzba> Insert(NarudzbaInsertRequest insert)
        {
            int statusId;

            if (insert.StatusNarudzbeId.HasValue)
            {
                statusId = insert.StatusNarudzbeId.Value;

            }
            else
            {
                var naziv = string.IsNullOrWhiteSpace(insert.StatusNarudzbe)
                    ? "Kreirana"
                    : insert.StatusNarudzbe!;

                statusId = await _context.Statuses
                                         .Where(s => s.Naziv == naziv)
                                         .Select(s => s.Id)
                                         .FirstOrDefaultAsync();

                if (statusId == 0)
                {
                    var s = new Status { Naziv = naziv };
                    _context.Statuses.Add(s);
                    await _context.SaveChangesAsync();
                    statusId = s.Id;
                }

                insert.StatusNarudzbeId = statusId;
            }

            var entity = _mapper.Map<Narudzba>(insert);
            entity.DatumNarudzbe ??= DateTime.Now;
            entity.StateMachine ??= "initial";
            entity.StatusNarudzbeId = statusId;

            _context.Narudzbas.Add(entity);
            await _context.SaveChangesAsync();

            //SendEmailOnTerminInsert(entity.KorisnikId);

            return _mapper.Map<Model.Narudzba>(entity);
        }


        public async Task<Model.Narudzba> Checkout(NarudzbaCheckoutRequest req)
        {
            if (req == null || req.KorisnikId <= 0 || req.Stavke == null || req.Stavke.Count == 0)
                throw new ArgumentException("Prazna ili neispravna narudžba.");

            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync<Model.Narudzba>(async () =>
            {
                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    var nar = new Database.Narudzba
                    {
                        DatumNarudzbe = req.DatumNarudzbe ?? DateTime.Now,
                        KorisnikId = req.KorisnikId,
                        StatusNarudzbeId = req.StatusNarudzbeId ?? 1,
                        StateMachine = "Kreirana"
                    };

                    _context.Narudzbas.Add(nar);
                    await _context.SaveChangesAsync();

                    foreach (var s in req.Stavke)
                    {
                        var jelo = await _context.Jelos
                            .Where(j => j.JeloId == s.JeloId)
                            .Select(j => new { j.JeloId, j.Cijena })
                            .SingleOrDefaultAsync();

                        if (jelo == null)
                            throw new Exception($"Jelo (ID={s.JeloId}) ne postoji.");

                        var cijenaInt = (int)Math.Round((double)(jelo.Cijena ?? 0m));

                        _context.StavkeNarudzbes.Add(new Database.StavkeNarudzbe
                        {
                            NarudzbaId = nar.Id,
                            JeloId = s.JeloId,
                            Kolicina = s.Kolicina,
                            Cijena = cijenaInt
                        });
                    }

                    await _context.SaveChangesAsync();
                    await tx.CommitAsync();

                    await _context.Entry(nar).Collection(n => n.StavkeNarudzbes).LoadAsync();

                    return _mapper.Map<Model.Narudzba>(nar);
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<int> CheckoutFromCart(int korisnikId, int? statusId = null, string? paymentId = null, DateTime? datumNarudzbe = null)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync<int>(async () =>
            {
                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    Status status;
                    if (statusId.HasValue)
                    {
                        status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == statusId.Value)
                                 ?? throw new ArgumentException($"Status {statusId.Value} ne postoji.");
                    }
                    else
                    {
                        status = await _context.Statuses.FirstOrDefaultAsync(s => s.Naziv == "Kreirana");
                        if (status == null)
                        {
                            status = new Status { Naziv = "Kreirana" };
                            _context.Statuses.Add(status);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var stavkeKorpe = await _context.Korpas
                        .Where(k => k.KorisnikId == korisnikId)
                        .ToListAsync();

                    if (!stavkeKorpe.Any())
                        throw new InvalidOperationException("Korpa je prazna.");

                    var narudzba = new Narudzba
                    {
                        KorisnikId = korisnikId,
                        DatumNarudzbe = datumNarudzbe ?? DateTime.Now,
                        StatusNarudzbeId = status.Id,
                        StateMachine = "Kreirana",
                        StavkeNarudzbes = stavkeKorpe.Select(k => new StavkeNarudzbe
                        {
                            JeloId = k.JeloId,
                            Kolicina = k.Kolicina ?? 1,
                            Cijena = (int)Math.Round((double)(k.Cijena ?? 0m))
                        }).ToList(),
                        PaymentId = paymentId

                    };

                    _context.Narudzbas.Add(narudzba);
                    await _context.SaveChangesAsync();

                    _context.Korpas.RemoveRange(stavkeKorpe);
                    await _context.SaveChangesAsync();

                    await tx.CommitAsync();
                    return narudzba.Id;
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            });
        }




        /* public override Task<Model.Narudzba> Insert(NarudzbaInsertRequest insert)
         {
             var state = _baseState.CreateState("initial");

             return state.Insert(insert);

         }*/

        public override async Task<Model.Narudzba> Update(int id, NarudzbaUpdateRequest update)
        {
            var entity = await _context.Narudzbas.FindAsync(id);

            var state = _baseState.CreateState(entity.StateMachine);

            return await state.Update(id, update);
        }

        public async Task<Model.Narudzba> Activate(int id)
        {
            var entity = await _context.Narudzbas.FindAsync(id);

            var state = _baseState.CreateState(entity.StateMachine);

            return await state.Activate(id);
        }

        public async Task<Model.Narudzba> Hide(int id)
        {
            var entity = await _context.Narudzbas.FindAsync(id);

            var state = _baseState.CreateState(entity.StateMachine);

            return await state.Hide(id);
        }

        public async Task<List<string>> AllowedActions(int id)
        {
            var entity = await _context.Narudzbas.FindAsync(id);
            var state = _baseState.CreateState(entity?.StateMachine ?? "initial");
            return await state.AllowedActions();
        }

        static MLContext mlContext = null;
        static object isLocked = new object();
        static ITransformer model = null;

        /*public List<Model.Narudzba> Recommend(int id)
        {
            lock (isLocked)
            {
                if (mlContext == null)
                {
                    mlContext = new MLContext();

                    var tmpData = _context.Narudzbas.Include("StavkeNarudzbes").ToList();

                    var data = new List<ProductEntry>();


                    foreach (var x in tmpData)
                    {
                        if (x.StavkeNarudzbes.Count > 1)
                        {
                            var distinctItemId = x.StavkeNarudzbes.Select(y => y.JeloId).ToList();

                            distinctItemId.ForEach(y =>
                            {
                                var relatedItems = x.StavkeNarudzbes.Where(z => z.JeloId != y);

                                foreach (var z in relatedItems)
                                {
                                    data.Add(new ProductEntry()
                                    {
                                        ProductID = (uint)y,
                                        CoPurchaseProductID = (uint)z.JeloId,
                                    });
                                }
                            });
                        }
                    }


                    var traindata = mlContext.Data.LoadFromEnumerable(data);

                    //STEP 3: Your data is already encoded so all you need to do is specify options for MatrxiFactorizationTrainer with a few extra hyperparameters
                    //        LossFunction, Alpa, Lambda and a few others like K and C as shown below and call the trainer.
                    MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
                    options.MatrixColumnIndexColumnName = nameof(ProductEntry.ProductID);
                    options.MatrixRowIndexColumnName = nameof(ProductEntry.CoPurchaseProductID);
                    options.LabelColumnName = "Label";
                    options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
                    options.Alpha = 0.01;
                    options.Lambda = 0.025;
                    // For better results use the following parameters
                    options.NumberOfIterations = 100;
                    options.C = 0.00001;

                    var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);

                    model = est.Fit(traindata);

                }
            }




            //prediction

            var products = _context.Proizvodis.Where(x => x.ProizvodId != id);

            var predictionResult = new List<Tuple<Database.Proizvodi, float>>();

            foreach (var product in products)
            {

                var predictionengine = mlContext.Model.CreatePredictionEngine<ProductEntry, Copurchase_prediction>(model);
                var prediction = predictionengine.Predict(
                                         new ProductEntry()
                                         {
                                             ProductID = (uint)id,
                                             CoPurchaseProductID = (uint)product.ProizvodId
                                         });


                predictionResult.Add(new Tuple<Database.Proizvodi, float>(product, prediction.Score));
            }


            var finalResult = predictionResult.OrderByDescending(x => x.Item2).Select(x => x.Item1).Take(3).ToList();

            return _mapper.Map<List<Model.Proizvodi>>(finalResult);

        }*/

    }

    public class Copurchase_prediction
    {
        public float Score { get; set; }
    }

    public class ProductEntry
    {
        [KeyType(count: 10)]
        public uint ProductID { get; set; }

        [KeyType(count: 10)]
        public uint CoPurchaseProductID { get; set; }

        public float Label { get; set; }
    }
}
