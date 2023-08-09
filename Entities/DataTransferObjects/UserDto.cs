using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
        public string? Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }


    }
}
