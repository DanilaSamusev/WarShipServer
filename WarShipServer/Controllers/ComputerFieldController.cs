using Microsoft.AspNetCore.Mvc;
using WarShipServer.Models;
using WarShipServer.Services;

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
            _game = game;
        }

        [HttpGet]
        public IActionResult GetFieldData()
        {
            Field computerField = _game.ComputerField;

            if (computerField != null)
            {
                return Ok(computerField.Squares);
            }

            return NotFound("Computer field is not found");
        }

        [HttpPut("makePlayerShot")]
        public IActionResult MakeShooting([FromQuery] int id)
        {
            /*if (!_game.IsPlayerTurn || _game.PlayerField.Fleet.ShipsOnField != 10)
            {
                return Ok();
            }*/
            Square[][] shotSquares= _game.MakeShooting(id);
            return Ok(shotSquares);
        }

        
    }
}