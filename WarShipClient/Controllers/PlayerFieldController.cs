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
        private static int _shipNumber;

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
        public IActionResult UpdateField([FromBody] Square checkedSquare)
        {
            Square[] possibleSquares = CheckSquares(checkedSquare);

            return Ok(possibleSquares);
        }

        [HttpPut("setShip")]
        
        public IActionResult HandleClick([FromBody] Square clickedSquare)
        {
            PossiblePointsCreature creature = new PossiblePointsCreature();
            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] points = creature.GetPossiblePoints(currentShip, clickedSquare.Id, 0);
            Square[] squares = new Square[currentShip.Decks.Length];
            
            PointsChecker checker = new PointsChecker(_playerField);
            ShipsAligner aligner = new ShipsAligner(_playerField, PlayerField.Fleet);

            if (checker.CheckPoints(points, 0))
            {
                aligner.SetShip(currentShip, points);
                _shipNumber++;
            }

            foreach (int point in points)
            {
                squares[point] = _playerField.Squares[point];
            }
            
            return Ok(squares);
        }

        private Square[] CheckSquares(Square checkedSquare)
        {
            PossiblePointsCreature creature = new PossiblePointsCreature();
            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] points = creature.GetPossiblePoints(currentShip, checkedSquare.Id, 0);

            Square[] squares = new Square[currentShip.Decks.Length];

            for (int i = 0; i < points.Length; i++)
            {
                _playerField.Squares[points[i]].IsChecked = !_playerField.Squares[points[i]].IsChecked;
                squares[i] = _playerField.Squares[points[i]];
            }

            return squares;
        }
    }
}