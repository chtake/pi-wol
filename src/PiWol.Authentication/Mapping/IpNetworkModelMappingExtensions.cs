using System.Collections.Generic;
using System.Linq;
using PiWol.Authentication.Abstraction.Models;
using PiWol.Authentication.Data.Abstraction.Entities;

namespace PiWol.Authentication.Mapping
{
    internal static class IpNetworkModelMappingExtensions
    {
        public static IpNetworkModel ToModel(this IpEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new IpNetworkModel
            {
                IpNetwork = entity.IpNetwork
            };
        }

        public static IEnumerable<IpNetworkModel> ToModels(this IEnumerable<IpEntity> entities)
        {
            return entities?.Select(x => x.ToModel());
        }

        public static IpEntity ToEntity(this IpNetworkModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new IpEntity
            {
                IpNetwork = model.IpNetwork
            };
        }

        public static IEnumerable<IpEntity> ToEntities(this IEnumerable<IpNetworkModel> entities)
        {
            return entities?.Select(x => x.ToEntity());
        }
    }
}