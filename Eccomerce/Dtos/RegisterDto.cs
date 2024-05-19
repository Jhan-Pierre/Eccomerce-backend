using System.ComponentModel.DataAnnotations;

namespace Eccomerce.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        public List<string>? Roles { get; set; }

    }
}
