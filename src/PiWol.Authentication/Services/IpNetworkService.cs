using System.Collections.Generic;
using System.Threading.Tasks;
using PiWol.Authentication.Abstraction.Models;
using PiWol.Authentication.Abstraction.Services;
using PiWol.Authentication.Data.Abstraction.Repositories;
using PiWol.Authentication.Mapping;

namespace PiWol.Authentication.Services
{
    internal class IpNetworkService : IIpNetworkService
    {
        private readonly IIpInNetworkValidatorService _parseService;
        private readonly IIpNetworkRepository _repository;

        public IpNetworkService(IIpNetworkRepository repository, IIpInNetworkValidatorService parseService)
        {
            _repository = repository;
            _parseService = parseService;
        }

        public async Task<IEnumerable<IpNetworkModel>> GetAll()
        {
            return await Task.FromResult(_repository.ReadAll().ToModels()).ConfigureAwait(false);
        }

        public async Task<IpNetworkModel> Delete(string ipNetwork)
        {
            var entity = _repository.Delete(ipNetwork);

            return await Task.FromResult(entity.ToModel()).ConfigureAwait(false);
        }

        public async Task<IpNetworkModel> Create(IpNetworkModel model)
        {
            var entity = _repository.Create(model.ToEntity());
            entity.IpNetwork = Parse(model);
            return await Task.FromResult(entity.ToModel()).ConfigureAwait(false);
        }

        public string Parse(IpNetworkModel model)
        {
            return _parseService.ParseRange(model.IpNetwork);
        }
    }
}