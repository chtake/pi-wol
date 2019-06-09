using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiWol.WakeOnLanService.Abstraction.Models;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.Data.Abstraction.Repositories;
using PiWol.WakeOnLanService.Mapping;

namespace PiWol.WakeOnLanService.Services
{
    public class HostService : IHostService
    {
        private readonly IHostStatusCacheService _cacheService;

        private readonly IHostRepository _repository;

        public HostService(IHostRepository repository, IHostStatusCacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<HostModel> Create(HostModel model)
        {
            var created = _repository.Create(model.ToEntity()).ToModel();
            created.Status = _cacheService.Get(created.IpAddress);

            return await Task.FromResult(created).ConfigureAwait(false);
        }

        public async Task<IEnumerable<HostModel>> ReadAll()
        {
            var models = _repository.ReadAll().ToModels().ToList();

            foreach (var model in models)
            {
                model.Status = _cacheService.Get(model.IpAddress);
            }

            return await Task.FromResult(models).ConfigureAwait(false);
        }

        public async Task<HostModel> Read(Guid id)
        {
            var entity = _repository.Read(id);
            var model = entity.ToModel();

            model.Status = _cacheService.Get(model.IpAddress);

            return await Task.FromResult(model).ConfigureAwait(false);
        }

        public async Task<HostModel> Update(HostModel model)
        {
            var entity = _repository.Update(model.Id, model.ToEntity());
            var m = entity.ToModel();

            m.Status = _cacheService.Get(m.IpAddress);

            return await Task.FromResult(m).ConfigureAwait(false);
        }

        public async Task<HostModel> Delete(Guid id)
        {
            var entity = _repository.Delete(id);
            return await Task.FromResult(entity.ToModel()).ConfigureAwait(false);
        }
    }
}