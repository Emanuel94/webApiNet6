using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetAllOwners();
        Owner GetOwnerById(Guid ownerId);

        Owner GetOwnerWithDetails(Guid ownerId);
        
        void  CreateOwner(Owner owner);
        void UpdateOwner(Owner owner);
            
        void DeleteOwner(Owner ownerId);

    }
}
