using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get; }

        IAccountRepository Account { get; }

        IUserRepository User { get; }

        void Save();

    }
}
