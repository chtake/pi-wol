using PiWol.Authentication.Abstraction.Models;
using PiWol.Authentication.WebApp.Dtos;

namespace PiWol.Authentication.WebApp.Mapping
{
    public static class PostIpNetworkDtoMappingExtensions
    {
        public static IpNetworkModel ToModel(this PostIpNetworkDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new IpNetworkModel
            {
                IpNetwork = dto.IpNetwork
            };
        }
    }
}