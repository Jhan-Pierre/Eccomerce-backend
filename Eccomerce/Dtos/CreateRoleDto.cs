using System.ComponentModel.DataAnnotations;

namespace Eccomerce.Dtos
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "El nombre del rol es requerido")]
        public string RoleName { get; set; } = null!;
    }
}
