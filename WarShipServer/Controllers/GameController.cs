using Microsoft.AspNetCore.Mvc;
using WarShipServer.Models;

namespace WarShipServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class GameController : Controller
    {
        private readonly GameData _gameData;

        public GameController(Game game)
        {
            if (_gameData == null)
            {
                _gameData = new GameData(game);
            }
        }
        
        [HttpGet]
        public IActionResult GetGameData()
        {
            return Ok(_gameData);
        }
    }
}