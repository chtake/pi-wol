using System.Collections.Generic;
using System.Linq;
using PiWol.WakeOnLanService.Abstraction.Models;
using PiWol.WakeOnLanService.Data.Abstraction.Entities;

namespace PiWol.WakeOnLanService.Mapping
{
    public static class HostModelMappingExtensions
    {
        public static HostModel ToModel(this HostEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new HostModel
            {
                Id = entity.Id,
                Hostname = entity.Hostname,
                IpAddress = entity.IpAddress,
                MacAddress = entity.MacAddress,
                Netmask = entity.Netmask
            };
        }

        public static IEnumerable<HostModel> ToModels(this IEnumerable<HostEntity> entities)
        {
            return entities?.Select(x => x.ToModel());
        }

        public static HostEntity ToEntity(this HostModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new HostEntity
            {
                Id = model.Id,
                IpAddress = model.IpAddress,
                MacAddress = model.MacAddress,
                Hostname = model.Hostname,
                Netmask = model.Netmask
            };
        }

        public static IEnumerable<HostEntity> ToEntities(this IEnumerable<HostModel> entities)
        {
            return entities?.Select(x => x.ToEntity());
        }
    }
}