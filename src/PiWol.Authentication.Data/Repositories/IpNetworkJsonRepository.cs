using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using PiWol.Authentication.Data.Abstraction.Entities;
using PiWol.Authentication.Data.Abstraction.Repositories;
using PiWol.DataStorage.Core;

namespace PiWol.Authentication.Data.Repositories
{
    internal class IpNetworkJsonRepository : JsonRepositoryBase<IpEntity>, IIpNetworkRepository
    {
        private static readonly object LockObj = new object();

        public IpNetworkJsonRepository(IFileProvider fileProvider) : base(fileProvider)
        {
        }

        protected override string DataFile { get; set; } = "auth-ips.json";


        public IpEntity Create(IpEntity entity)
        {
            lock (LockObj)
            {
                Data.Add(entity);

                WriteToFile();

                return Read(entity.IpNetwork);
            }
        }

        public IpEntity Read(string ipNetwork)
        {
            return DataSource.FirstOrDefault(x => x.IpNetwork == ipNetwork);
        }

        public IEnumerable<IpEntity> ReadAll()
        {
            return DataSource.ToList();
        }

        public IpEntity Delete(string ipNetwork)
        {
            lock (LockObj)
            {
                var entity = Read(ipNetwork);
                if (entity == null)
                {
                    return null;
                }

                Data.Remove(entity);
                WriteToFile();
                return entity;
            }
        }
    }
}