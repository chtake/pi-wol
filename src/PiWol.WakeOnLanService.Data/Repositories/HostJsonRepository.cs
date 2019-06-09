using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using PiWol.DataStorage.Core;
using PiWol.WakeOnLanService.Data.Abstraction.Entities;
using PiWol.WakeOnLanService.Data.Abstraction.Exceptions;
using PiWol.WakeOnLanService.Data.Abstraction.Repositories;

namespace PiWol.WakeOnLanService.Data.Repositories
{
    internal class HostJsonRepository : JsonRepositoryBase<HostEntity>, IHostRepository
    {
        private static readonly object LockObj = new object();

        public HostJsonRepository(IFileProvider fileProvider) : base(fileProvider)
        {
        }

        protected override string DataFile { get; set; } = "hosts.json";


        public HostEntity Create(HostEntity entity)
        {
            lock (LockObj)
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                entity.CreatedDateTime = DateTime.Now;

                Data.Add(entity);

                WriteToFile();

                return Read(entity.Id);
            }
        }

        public HostEntity Read(Guid id)
        {
            return DataSource.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<HostEntity> ReadAll()
        {
            return DataSource.ToList();
        }

        public HostEntity Update(Guid id, HostEntity entity)
        {
            lock (LockObj)
            {
                var source = Data.FirstOrDefault(x => x.Id == id);

                if (source == null)
                {
                    throw new EntityNotFoundException();
                }

                source.LastModifiedDateTime = DateTime.Now;
                source.Hostname = entity.Hostname;
                source.IpAddress = entity.IpAddress;
                source.MacAddress = entity.MacAddress;
                source.Netmask = entity.Netmask;

                WriteToFile();

                return Read(id);
            }
        }

        public HostEntity Delete(Guid id)
        {
            lock (LockObj)
            {
                var entity = Read(id);
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