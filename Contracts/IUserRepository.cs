using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(Guid UserId);

        void CreateUser(User user); 

        void UpdateUser(User user);
        void DeleteUser (User user);

    }
}
