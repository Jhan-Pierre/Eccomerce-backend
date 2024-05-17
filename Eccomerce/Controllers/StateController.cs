using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Eccomerce.Models;

namespace Eccomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly DbEccomerceContext _context;

        public StateController(DbEccomerceContext context)
        {
            _context = context;
        }

        //Listar estados
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var listStates = await _context.TbStates.ToListAsync();
            return Ok(listStates);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] TbState request)
        {
            await _context.TbStates.AddAsync(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stateDelete = await _context.TbStates.FindAsync(id);

            if (stateDelete == null)
            {
                return BadRequest("No existe el estado");
            }

            _context.TbStates.Remove(stateDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
