using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return FindAll().
                OrderBy(owner => owner.Name).
                ToList();
        }
        public Owner GetOwnerById(Guid ownerId)
        {
            return FindByCondition(own => own.Id.Equals(ownerId))
                .FirstOrDefault();
        }

        public Owner GetOwnerWithDetails(Guid ownerId)
        {
            return FindByCondition(owner => owner.Id.Equals(ownerId))
                .Include(ac => ac.Accounts)
                .FirstOrDefault();
        }
        public void CreateOwner(Owner owner) => Create(owner);

        public void UpdateOwner(Owner owner) => Update(owner);
    
        public void DeleteOwner(Owner ownerId) => Delete(ownerId);
    }
}
