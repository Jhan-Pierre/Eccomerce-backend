using Eccomerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eccomerce.Controllers
{
    [Route("api/[controller]")] // api/students
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbEccomerceContext _context;

        public UsersController(DbEccomerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<TbUser>> getUsers()
        {
            var users = await _context.TbUsers.AsNoTracking().ToListAsync();

            return users;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TbUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.AddAsync(user);

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok(result);
            }

            return BadRequest();
        }

    }
}
