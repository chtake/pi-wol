using System.Threading.Tasks;

namespace PiWol.WakeOnLanService.Abstraction.Services
{
    public interface IMacAddressResolverService
    {
        Task<string> Resolve(string ipAddr);
    }
}