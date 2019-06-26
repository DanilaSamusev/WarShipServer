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
        private static Field PlayerField { get; set; }
        private readonly PossiblePointsCreature _possiblePointsCreature;
        private readonly PointsValidator _pointsValidator;
        private readonly SquaresManager _squaresManager;
        private readonly PointsManager _pointsManager;
        private static int _currentShipNumber;
        private static int[] _previousPoints;

        public PlayerFieldController(PossiblePointsCreature possiblePointsCreature, PointsValidator pointsValidator,
            SquaresManager squaresManager, PointsManager pointsManager)
        {
            _possiblePointsCreature = possiblePointsCreature;
            _pointsValidator = pointsValidator;
            _squaresManager = squaresManager;
            _pointsManager = pointsManager;
        }

        [HttpGet]
        public IActionResult GetSquares()
        {
            if (PlayerField == null)
            {
                PlayerField = new Field();
            }
            
            return Ok(PlayerField.Squares);
        }

        [HttpPut("markSquaresForShip")]
        public IActionResult MarkSquaresForShip([FromQuery] int id, int direction)
        {
            if (_previousPoints != null)
            {
                _squaresManager.SetIsChecked(PlayerField, _previousPoints, false);
            }

            Ship currentShip = PlayerField.Fleet.Ships[_currentShipNumber];
            int[] possiblePoints = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);

            if (_pointsValidator.ValidatePoints(PlayerField, possiblePoints, direction))
            {
                _squaresManager.SetIsChecked(PlayerField, possiblePoints, true);

                int[] changedPoints;
                if (_previousPoints == null)
                {
                    changedPoints = possiblePoints;
                }
                else
                {
                    changedPoints = _pointsManager.JoinPoints(possiblePoints, _previousPoints);
                }

                _previousPoints = possiblePoints;

                return Ok(_squaresManager.GetSquaresByPoints(PlayerField, changedPoints));
            }

            return Ok(_squaresManager.GetSquaresByPoints(PlayerField, _previousPoints));
        }
        
        [HttpPut("plantShip")]
        public IActionResult HandleClick([FromQuery] int id, int direction)
        {
            if (_currentShipNumber >= 10)
            {
                return Ok();
            }
            
            Ship currentShip = PlayerField.Fleet.Ships[_currentShipNumber];
            int[] points = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);
            Square[] squares = new Square[currentShip.Decks.Length];

            ShipsAligner aligner = new ShipsAligner();
            
            if (_pointsValidator.ValidatePoints(PlayerField, points, direction))
            {
                aligner.SetShip(currentShip, points, PlayerField);
                _currentShipNumber++;
                
                for (int i = 0; i < points.Length; i++)
                {
                    squares[i] = PlayerField.Squares[points[i]];
                }
                
                return Ok(squares);
            }

            return Ok();
        }

        [HttpPut("makeShot")]
        public IActionResult MakeShot(int id)
        {
            PlayerField.Squares[id].IsClicked = true;            
            return Ok(PlayerField.Squares[id]);
        }
    }
}