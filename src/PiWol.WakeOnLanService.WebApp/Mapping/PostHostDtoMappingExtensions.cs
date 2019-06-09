using PiWol.WakeOnLanService.Abstraction.Models;
using PiWol.WakeOnLanService.WebApp.Dto;

namespace PiWol.WakeOnLanService.WebApp.Mapping
{
    public static class PostHostDtoMappingExtensions
    {
        public static HostModel ToModel(this PostHostDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new HostModel { IpAddress = dto.IpAddress, Hostname = dto.Hostname, MacAddress = dto.MacAddress, Netmask = dto.Netmask };
        }
    }
}