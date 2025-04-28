using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "First name cannot be longer than 25 characters.")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(25, ErrorMessage = "Last name cannot be longer than 25 characters.")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; } = string.Empty;
    }
}
