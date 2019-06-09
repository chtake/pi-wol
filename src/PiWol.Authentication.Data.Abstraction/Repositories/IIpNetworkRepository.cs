using System.Collections.Generic;
using PiWol.Authentication.Data.Abstraction.Entities;

namespace PiWol.Authentication.Data.Abstraction.Repositories
{
    public interface IIpNetworkRepository
    {
        IpEntity Create(IpEntity entity);
        IpEntity Read(string ipNetwork);
        IEnumerable<IpEntity> ReadAll();
        IpEntity Delete(string ipNetwork);
    }
}