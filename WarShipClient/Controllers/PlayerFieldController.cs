using Microsoft.AspNetCore.Mvc;
using WarShipClient.Models;
using WarShipClient.Services;

namespace WarShipClient.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerFieldController : Controller
    {
        private static Field _playerField;
        private static int _shipNumber = 0;

        public PlayerFieldController()
        {
            _playerField = PlayerField.NewPlayerField();
        }

        [HttpGet]
        public IActionResult GetField()
        {
            return Ok(_playerField);
        }

        [HttpPut]
        public IActionResult UpdateField([FromBody] Square square)
        {           
            Square[] squares = new Square[PlayerField.Fleet.Ships[_shipNumber].Decks.Length];
            PossiblePointsCreature creature = new PossiblePointsCreature();
            int[] points = creature.GetPossiblePoints(PlayerField.Fleet.Ships[_shipNumber], square.Id, 0);

            for (int i = 0; i < points.Length; i++)
            {
                _playerField.Squares[points[i]].IsChecked = !_playerField.Squares[points[i]].IsChecked;
                squares[i] = _playerField.Squares[points[i]];
            }

            return Ok(squares);
        }
        
    }
}