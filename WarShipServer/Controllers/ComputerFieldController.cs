using Microsoft.AspNetCore.Mvc;
using WarShipServer.Models;

namespace WarShipServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerFieldController : Controller
    {
        
        [HttpPut("playerShot")]
        public IActionResult MakePlayerShot([FromQuery] int id)
        {
            return Ok();
        }

        
    }
}