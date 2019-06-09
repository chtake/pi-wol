using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PiWol.WakeOnLanService.Abstraction.Models;

namespace PiWol.WakeOnLanService.Abstraction.Services
{
    public interface IHostService
    {
        Task<HostModel> Create(HostModel model);

        Task<IEnumerable<HostModel>> ReadAll();

        Task<HostModel> Read(Guid id);

        Task<HostModel> Update(HostModel model);

        Task<HostModel> Delete(Guid id);
    }
}