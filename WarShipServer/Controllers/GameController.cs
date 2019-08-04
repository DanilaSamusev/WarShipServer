using Microsoft.AspNetCore.Mvc;
using WarShipServer.Models;
using WarShipServer.Services;

namespace WarShipServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class GameController : Controller
    {
        [HttpGet]
        public IActionResult GetGameData()
        {
            return Ok(new Game(new ShipsAligner()));
        }
    }
}