using Eccomerce.Dtos;
using Eccomerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eccomerce.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;  // Gestiona operaciones de usuario
        private readonly RoleManager<IdentityRole> _roleManager;  // Gestiona operaciones de roles
        private readonly IConfiguration _configuration;  // Accede a configuraciones de la aplicación

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Devuelve un error si el modelo no es válido
            }

            var user = new AppUser
            {
                Email = registerDto.Email,
                fullName = registerDto.FullName,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);  // Crea un nuevo usuario

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);  // Devuelve un error si la creación del usuario falla
            }

            // Si no se especifican roles, asigna el rol "User" por defecto
            if (registerDto.Roles is null)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                // Asigna los roles especificados al usuario
                foreach (var role in registerDto.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }

            return Ok(new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Cuenta creada exitosamente"
            });

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Devuelve un error si el modelo no es válido
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);  // Busca al usuario por su email

            if (user is null)
            {
                return Unauthorized(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Usuario no registrado"
                });
            }

            bool result = await _userManager.CheckPasswordAsync(user, loginDto.Password);  // Verifica la contraseña

            if (!result)
            {
                return Unauthorized(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Contraseña invalida"
                });
            }

            var token = GenerateToken(user);  // Genera el token JWT

            return Ok(new AuthResponseDto
            {
                Token = token,
                IsSuccess = true,
                Message = "Login exitoso"
            });
        }

        private string GenerateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Obtiene la clave de seguridad desde la configuración
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWTSetting").GetSection("securityKey").Value!);

            // Obtiene los roles del usuario
            var roles = _userManager.GetRolesAsync(user).Result;

            // Define las claims del token
            List<Claim> claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Email, user.Email??""),
                new (JwtRegisteredClaimNames.Name, user.fullName??""),
                new (JwtRegisteredClaimNames.NameId, user.Id??""),
                new (JwtRegisteredClaimNames.Aud, _configuration.GetSection("JWTSetting").GetSection("ValidAudience").Value!),
                new (JwtRegisteredClaimNames.Iss, _configuration.GetSection("JWTSetting").GetSection("ValidIssuer").Value!)
            };

            // Añade los roles a las claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Configura el descriptor del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),  // El token expira en 1 día
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);  // Crea el token

            return tokenHandler.WriteToken(token);  // Devuelve el token como string
        }

        [Authorize]
        [HttpGet("detail")]
        public async Task<ActionResult<UserDetailDto>> GetUserDetail()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(currentUserId!);

            if (user is null)
            {
                return NotFound(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Usuario no existe"
                });
            }

            return Ok(new UserDetailDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.fullName,
                Roles = [.. await _userManager.GetRolesAsync(user)],
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                AccessFailedCount = user.AccessFailedCount,
            });

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailDto>>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDetailDtos = new List<UserDetailDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userDetailDto = new UserDetailDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.fullName,
                    Roles = roles.ToArray()
                };

                userDetailDtos.Add(userDetailDto);
            }

            return Ok(userDetailDtos);
        }


    }
}
