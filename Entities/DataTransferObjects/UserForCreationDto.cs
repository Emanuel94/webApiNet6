using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserForCreationDto
    {

        [Required(ErrorMessage = "User Name is requied")]
        [StringLength(50, ErrorMessage = "It cant'n be longer than 50  characters")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "It can't be loger than 100 ")]
        public string? Email { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required(ErrorMessage = "Password is Required ")]
        [StringLength(25, ErrorMessage = "It cant'n be loger than 20 character")]
        public string? Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
