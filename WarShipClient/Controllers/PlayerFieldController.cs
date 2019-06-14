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
        public static Field Field { get; set; }
        private static int _shipNumber;        

        public PlayerFieldController()
        {
            Field = PlayerField.NewPlayerField();
        }

        [HttpGet]
        public IActionResult GetSquares()
        {
            return Ok(Field.Squares);
        }

        [HttpPut("checkPoints")]
        public IActionResult HandleCursorOver([FromQuery] int id, int direction)
        {
            PossiblePointsCreature pointsCreature = new PossiblePointsCreature();
            PointsValidator pointsValidator = new PointsValidator(Field);
            SquaresManager squaresManager = new SquaresManager();
            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] possiblePoints = pointsCreature.GetPossiblePoints(currentShip, id, direction);

            if (pointsValidator.ValidatePoints(possiblePoints, direction))
            {
                squaresManager.SetIsChecked(Field, possiblePoints, true);                
            }

            return Ok(squaresManager.GetSquaresByPoints(Field, possiblePoints));
        }

        [HttpPut("mouseOut")]
        public IActionResult HandleCursorOut([FromQuery] int id, int direction)
        {
            PossiblePointsCreature pointsCreature = new PossiblePointsCreature();            
            SquaresManager squaresManager = new SquaresManager();
            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] possiblePoints = pointsCreature.GetPossiblePoints(currentShip, id, direction);
           
            squaresManager.SetIsChecked(Field, possiblePoints, false);                            

            return Ok(squaresManager.GetSquaresByPoints(Field, possiblePoints));
        }

        [HttpPut("setShip")]
        public IActionResult HandleClick([FromQuery] int id, int direction)
        {
            PossiblePointsCreature creature = new PossiblePointsCreature();
            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] points = creature.GetPossiblePoints(currentShip, id, direction);
            Square[] squares = new Square[currentShip.Decks.Length];

            PointsValidator validator = new PointsValidator(Field);
            ShipsAligner aligner = new ShipsAligner(Field, PlayerField.Fleet);

            if (validator.ValidatePoints(points, 0))
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