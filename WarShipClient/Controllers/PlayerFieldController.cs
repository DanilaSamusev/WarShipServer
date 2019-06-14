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

        public PlayerFieldController(PossiblePointsCreature possiblePointsCreature, PointsValidator pointsValidator, SquaresManager squaresManager)
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
            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] possiblePoints = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);

            if (_pointsValidator.ValidatePoints(Field, possiblePoints, direction))
            {
                _squaresManager.SetIsChecked(Field, possiblePoints, true);                
            }

            return Ok(_squaresManager.GetSquaresByPoints(Field, possiblePoints));
        }

        [HttpPut("mouseOut")]
        public IActionResult HandleCursorOut([FromQuery] int id, int direction)
        {            
            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] possiblePoints = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);
           
            _squaresManager.SetIsChecked(Field, possiblePoints, false);                            

            return Ok(_squaresManager.GetSquaresByPoints(Field, possiblePoints));
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