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
        private static Field Field { get; set; }
        private readonly PossiblePointsCreature _possiblePointsCreature;
        private readonly PointsValidator _pointsValidator;
        private readonly SquaresManager _squaresManager;
        private static int _shipNumber;
        private static int[] _cleanedPoints;

        public PlayerFieldController(PossiblePointsCreature possiblePointsCreature, PointsValidator pointsValidator,
            SquaresManager squaresManager)
        {
            Field = PlayerField.NewPlayerField();
            _possiblePointsCreature = possiblePointsCreature;
            _pointsValidator = pointsValidator;
            _squaresManager = squaresManager;
        }

        [HttpGet]
        public IActionResult GetSquares()
        {
            return Ok(Field.Squares);
        }

        [HttpPut("checkPoints")]
        public IActionResult HandleCursorOver([FromQuery] int id, int direction)
        {
            if (_cleanedPoints != null)
            {
                _squaresManager.SetIsChecked(Field, _cleanedPoints, false);
            }

            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] possiblePoints = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);

            if (_pointsValidator.ValidatePoints(Field, possiblePoints, direction))
            {
                _squaresManager.SetIsChecked(Field, possiblePoints, true);
            }

            int[] changedPoints = HandleCursorOut(possiblePoints);
            _cleanedPoints = possiblePoints;
            
            return Ok(_squaresManager.GetSquaresByPoints(Field, changedPoints));
        }
        
        public int[] HandleCursorOut(int[] possiblePoints)
        {
            int[] changedPoints;

            if (_cleanedPoints == null)
            {
                changedPoints = possiblePoints;
            }
            else
            {
                changedPoints = new int[_cleanedPoints.Length + possiblePoints.Length];
                possiblePoints.CopyTo(changedPoints, 0);
                _cleanedPoints.CopyTo(changedPoints, possiblePoints.Length);                
            }

            return changedPoints;
        }

        [HttpPut("setShip")]
        public IActionResult HandleClick([FromQuery] int id, int direction)
        {
            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] points = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);
            Square[] squares = new Square[currentShip.Decks.Length];

            ShipsAligner aligner = new ShipsAligner(Field, PlayerField.Fleet);

            if (_pointsValidator.ValidatePoints(Field, points, direction))
            {
                aligner.SetShip(currentShip, points);
                _shipNumber++;
            }

            for (int i = 0; i < points.Length; i++)
            {
                squares[i] = Field.Squares[points[i]];
            }

            return Ok(squares);
        }
    }
}