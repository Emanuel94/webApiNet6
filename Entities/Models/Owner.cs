
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("owner")]
    public class Owner
    {
        [Column("OwnerId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage=" Name is Required")]
        [StringLength(50, ErrorMessage ="Name Can't be longer than 60 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Data of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage ="Address is required")]
        [StringLength(100, ErrorMessage ="Address can't be longer than 100 characters")]
        public string? Address { get; set; }

        public ICollection<Account>? Accounts { get; set; }
    }
}
