using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class OwnerForCreationDto
    {
        [Required(ErrorMessage = " Name is Required")]
        [StringLength(50, ErrorMessage = "Name Can't be longer tha 60 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Data of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can't be longer then 100 characters")]
        public string? Address { get; set; }
    }
}
