using System;
using System.Collections.Generic;
using PiWol.WakeOnLanService.Data.Abstraction.Entities;

namespace PiWol.WakeOnLanService.Data.Abstraction.Repositories
{
    public interface IHostRepository
    {
        HostEntity Create(HostEntity entity);

        HostEntity Read(Guid id);

        IEnumerable<HostEntity> ReadAll();

        HostEntity Update(Guid id, HostEntity entity);

        HostEntity Delete(Guid id);
    }
}