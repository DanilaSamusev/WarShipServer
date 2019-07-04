using Microsoft.AspNetCore.Mvc;
using WarShipServer.Models;

namespace WarShipServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class GameController : Controller
    {
        [HttpGet]
        public IActionResult GetGameData(Game game)
        {
            if (game == null)
            {
                return NotFound();
            }
            
            return Ok(game);
        }
    }
}