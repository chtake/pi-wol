using System.Collections.Generic;
using System.Threading.Tasks;
using PiWol.Authentication.Abstraction.Models;

namespace PiWol.Authentication.Abstraction.Services
{
    public interface IIpNetworkService
    {
        Task<IEnumerable<IpNetworkModel>> GetAll();
        Task<IpNetworkModel> Delete(string ipNetwork);
        Task<IpNetworkModel> Create(IpNetworkModel model);
        string Parse(IpNetworkModel model);
    }
}