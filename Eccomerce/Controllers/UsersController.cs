using Eccomerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eccomerce.Controllers
{
    //[Route("api/[controller]")] // api/students
    //[ApiController]
    //public class UsersController : ControllerBase
    //{
    //    private readonly DbEccomerceContext _context;

    //    public UsersController(DbEccomerceContext context)
    //    {
    //        _context = context;
    //    }

    //    [HttpGet]
    //    public async Task<IEnumerable<TbUser>> getUsers()
    //    {
    //        var users = await _context.TbUsers.AsNoTracking().ToListAsync();

    //        return users;
    //    }

    //    [HttpGet("{id:long}")]
    //    public async Task<ActionResult<TbUser>> getUser(long id)
    //    {
    //        var user = await _context.TbUsers.FindAsync(id);

    //        if(user is null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(user);
    //    }


    //    [HttpPost]
    //    public async Task<IActionResult> Create(TbUser user)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        await _context.AddAsync(user);

    //        var result = await _context.SaveChangesAsync();

    //        if(result > 0)
    //        {
    //            return Ok(result);
    //        }

    //        return BadRequest();
    //    }

    //    [HttpDelete("{id:long}")]
    //    public async Task<IActionResult> Delete(long id)
    //    {
    //        var user = await _context.TbUsers.FindAsync(id);

    //        if (user is null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Remove(user);

    //        var result = await _context.SaveChangesAsync();

    //        if (result > 0)
    //        {
    //            return Ok("Usuario Eliminado con exito");
    //        }


    //        return BadRequest("Incapaz de eliminar al usuario");

    //    }

    //    [HttpPut("{id:long}")]
    //    public async Task<IActionResult> Edit(long id, TbUser user)
    //    {
    //        var userFromDb = await _context.TbUsers.FindAsync(id);

    //        if (userFromDb is null)
    //        {
    //            return BadRequest("Usuario no encontrado");
    //        }

    //        // Actualizar solo los campos proporcionados en el objeto 'user'
    //        if (!string.IsNullOrEmpty(user.FirstName))
    //        {
    //            userFromDb.FirstName = user.FirstName;
    //        }

    //        if (!string.IsNullOrEmpty(user.LastName))
    //        {
    //            userFromDb.LastName = user.LastName;
    //        }

    //        if (!string.IsNullOrEmpty(user.Email))
    //        {
    //            userFromDb.Email = user.Email;
    //        }

    //        if (!string.IsNullOrEmpty(user.Password))
    //        {
    //            userFromDb.Password = user.Password;
    //        }

    //        if (!string.IsNullOrEmpty(user.Phone))
    //        {
    //            userFromDb.Phone = user.Phone;
    //        }

    //        if (user.RoleId != 0) 
    //        {
    //            userFromDb.RoleId = user.RoleId;
    //        }
    //        else
    //        {
    //            return BadRequest("RoleId es obligatorio");
    //        }

    //        if (user.StateId != 0) 
    //        {
    //            userFromDb.StateId = user.StateId;
    //        }
    //        else
    //        {
    //            return BadRequest("StateId es obligatorio");
    //        }

    //        if (user.ShiftId.HasValue)
    //        {
    //            userFromDb.ShiftId = user.ShiftId.Value;
    //        }

    //        userFromDb.UpdatedAt = DateTime.UtcNow; // Establecer la fecha y hora actual en UTC

    //        var result = await _context.SaveChangesAsync();

    //        if (result > 0)
    //        {
    //            return Ok("Usuario actualizado correctamente");
    //        }

    //        return BadRequest("No se pudo actualizar la información");
    //    }


    //}
}
