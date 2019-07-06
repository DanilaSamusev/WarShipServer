using Microsoft.AspNetCore.Mvc;
using WarShipServer.Models;

namespace WarShipServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerFieldController : Controller
    {
        private readonly Game _game;

        public ComputerFieldController(Game game)
        {
            if (_game == null)
            {
                _game = game;
            }
        }
        
        [HttpPut("playerShot")]
        public IActionResult MakePlayerShot([FromQuery] int id)
        {
            if (!_game.IsPlayerTurn || _game.PlayerField.Fleet.ShipsOnField != 10)
            {
                return Ok();
            }

            _game.ComputerField.Squares[id].IsClicked = true;

            if (!_game.ComputerField.Squares[id].HasShip)
            {
                _game.IsPlayerTurn = false;
            }
            
            return Ok(_game.ComputerField.Squares[id]);
        }

        
    }
}