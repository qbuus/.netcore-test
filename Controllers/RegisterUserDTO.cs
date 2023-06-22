using System.ComponentModel.DataAnnotations;
using API.entityFramework;

namespace API.entityFramework
{
    public class RegisterUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
       public string Password { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; } = 1;
    }
}