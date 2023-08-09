using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("userAuth")]
    public class User
    {
        [Column("UserId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User Name is requied")]
        [StringLength(50, ErrorMessage ="It cant'n be longer than 50  characters")]
        public string? UserName { get; set; }

        [Required(ErrorMessage ="Email is required.")]
        [StringLength(100, ErrorMessage ="It can't be loger than 100 ")]
        public string? Email { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required(ErrorMessage ="Password is Required ")]
        [StringLength(25, ErrorMessage ="It cant'n be loger than 20 character")]
        public string? Password { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }


    }
}
