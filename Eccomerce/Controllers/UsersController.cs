using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eccomerce.Controllers
{
    [Route("api/[controller]")] // api/students
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public string SayHi()
        {
            return "hi";
        }
    }
}
