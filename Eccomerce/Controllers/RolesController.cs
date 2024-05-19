using Eccomerce.Dtos;
using Eccomerce.Models;
using Eccomerce.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Eccomerce.Controllers
{
    [Authorize(Roles = $"{Roles.ADMIN}")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost] 
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            if (string.IsNullOrEmpty(createRoleDto.RoleName))
            {
                return BadRequest("El nombre del rol es requerido");
            }

            var roleExist = await _roleManager.RoleExistsAsync(createRoleDto.RoleName);
            if (roleExist)
            {
                return BadRequest("El rol ya existe");
            }

            var roleResult = await _roleManager.CreateAsync(new IdentityRole(createRoleDto.RoleName));

            if (roleResult.Succeeded)
            {
                return Ok(new {message="Rol creado exitosamente"});
            }

            return BadRequest("La creación del rol fallo");
        }

        [AllowAnonymous] //cambiar para la presentacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponseDto>>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var roleResponseDtos = new List<RoleResponseDto>();

            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
                var roleResponseDto = new RoleResponseDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    TotalUsers = usersInRole.Count
                };

                roleResponseDtos.Add(roleResponseDto);
            }

            return Ok(roleResponseDtos);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if(role is null)
            {
                return NotFound("Rol no encontrado");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok(new { message = "Rol eliminado exitosamente" });
            }

            return BadRequest("No se pudo eliminar el rol");
        }

        [HttpPost("assign")]
        public async Task<ActionResult> AssignRole([FromBody] RoleAssignDto roleAssignDto)
        {
            var user = await _userManager.FindByIdAsync(roleAssignDto.UserId);

            if(user is null)
            {
                return NotFound("Usuario no econtrado");
            }

            var role = await _roleManager.FindByIdAsync(roleAssignDto.RoleId);

            if(role is null)
            {
                return NotFound("Rol no encontrado");
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name!);

            if(result.Succeeded)
            {
                return Ok(new { message = "Rol asignado con exito" });
            }

            var error = result.Errors.FirstOrDefault();

            return BadRequest(error!.Description);

        }

        [HttpPost("remove")]
        public async Task<ActionResult> RemoveRole([FromBody] RoleAssignDto roleRemoveDto)
        {
            var user = await _userManager.FindByIdAsync(roleRemoveDto.UserId);

            if (user is null)
            {
                return NotFound("Usuario no encontrado");
            }

            var role = await _roleManager.FindByIdAsync(roleRemoveDto.RoleId);

            if (role is null)
            {
                return NotFound("Rol no encontrado");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name!);

            if (result.Succeeded)
            {
                return Ok(new { message = "Rol removido con exito" });
            }

            var error = result.Errors.FirstOrDefault();

            return BadRequest(error!.Description);
        }


    }
}
