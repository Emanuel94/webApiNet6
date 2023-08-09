using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .OrderBy(x=> x.UserName)
                .ToList();
        }

        public User GetUserById(Guid UserId)
        {
           return FindByCondition(user => user.Id.Equals(UserId))
                .FirstOrDefault();
        }
        public void CreateUser(User user)
        {
             Create(user);
        }
        public void UpdateUser(User user) =>  Update(user);
    

        public void DeleteUser(User user) =>  Delete(user); 
      
    }
}
