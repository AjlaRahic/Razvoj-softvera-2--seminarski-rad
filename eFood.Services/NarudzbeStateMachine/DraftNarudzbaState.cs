using AutoMapper;
using EasyNetQ;
using eFood.Model.Requests;
using eFood.Services.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.NarudzbeStateMachine
{
    public class DraftNarudzbeState : BaseNarudzbaState
    {
        protected ILogger<DraftNarudzbeState> _logger;
        public DraftNarudzbeState(ILogger<DraftNarudzbeState> logger, EFoodContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
            _logger = logger;
        }

        public override async Task<Model.Narudzba> Update(int id, NarudzbaUpdateRequest request)
        {
            var set = _context.Set<Database.Narudzba>();

            var entity = await set.FindAsync(id);

            _mapper.Map(request, entity);


            await _context.SaveChangesAsync();
            return _mapper.Map<Model.Narudzba>(entity);
        }

        public override async Task<Model.Narudzba> Activate(int id)
        {
            _logger.LogInformation($"Aktivacija proizvoda: {id}");

            _logger.LogWarning($"W: Aktivacija proizvoda: {id}");

            _logger.LogError($"E: Aktivacija proizvoda: {id}");

            var set = _context.Set<Database.Narudzba>();

            var entity = await set.FindAsync(id);

            entity.StateMachine = "active";

            await _context.SaveChangesAsync();


            var mappedEntity = _mapper.Map<Model.Narudzba>(entity);

            using var bus = RabbitHutch.CreateBus("host=localhost");

            bus.PubSub.Publish(mappedEntity);

            return mappedEntity;
        }

        public override async Task<List<string>> AllowedActions()
        {
            var list = await base.AllowedActions();

            list.Add("Update");
            list.Add("Activate");

            return list;
        }

    }
}
