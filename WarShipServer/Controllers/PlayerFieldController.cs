using Microsoft.AspNetCore.Mvc;
using WarShipServer.Models;
using WarShipServer.Services;

namespace WarShipServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerFieldController : Controller
    {
        private readonly Game _game;
        private readonly PossiblePointsCreature _possiblePointsCreature;
        private readonly PointsValidator _pointsValidator;
        private readonly SquaresManager _squaresManager;
        private readonly PointsManager _pointsManager;
        private static int _currentShipNumber;
        private static int[] _previousPoints;

        public PlayerFieldController(Game game, PossiblePointsCreature possiblePointsCreature,
            PointsValidator pointsValidator,
            SquaresManager squaresManager, PointsManager pointsManager)
        {
            _game = game;
            _possiblePointsCreature = possiblePointsCreature;
            _pointsValidator = pointsValidator;
            _squaresManager = squaresManager;
            _pointsManager = pointsManager;
        }

        [HttpGet]
        public IActionResult GetSquares()
        {
            Field playerField = _game.PlayerField;

            if (playerField != null)
            {
                return Ok(playerField.Squares);
            }

            return NotFound("Player field id not found");
        }

        [HttpPut("markSquaresForShipPlanting")]
        public IActionResult MarkSquaresForShipPlanting([FromQuery] int id, int direction)
        {
            if (_game.PlayerField.Fleet.ShipsOnField == 10)
            {
                return Ok();
            }

            if (_previousPoints != null)
            {
                _squaresManager.SetIsChecked(_game.PlayerField, _previousPoints, false);
            }

            Ship currentShip = _game.PlayerField.Fleet.Ships[_currentShipNumber];
            int[] possiblePoints = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);

            if (_pointsValidator.ValidatePoints(_game.PlayerField, possiblePoints, direction))
            {
                _squaresManager.SetIsChecked(_game.PlayerField, possiblePoints, true);

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

                return Ok(_squaresManager.GetSquaresByPoints(_game.PlayerField, changedPoints));
            }

            return Ok(_squaresManager.GetSquaresByPoints(_game.PlayerField, _previousPoints));
        }

        [HttpPut("plantShip")]
        public IActionResult PlantShip([FromQuery] int id, int direction)
        {
            if (_game.PlayerField.Fleet.ShipsOnField == 10)
            {
                return Ok();
            }

            Ship currentShip = _game.PlayerField.Fleet.Ships[_currentShipNumber];
            int[] points = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);
            Square[] squares = new Square[currentShip.Decks.Length];

            ShipsAligner aligner = new ShipsAligner();

            if (_pointsValidator.ValidatePoints(_game.PlayerField, points, direction))
            {
                aligner.PlantShip(currentShip, points, _game.PlayerField);
                _currentShipNumber++;

                for (int i = 0; i < points.Length; i++)
                {
                    squares[i] = _game.PlayerField.Squares[points[i]];
                }

                _game.PlayerField.Fleet.ShipsOnField++;
                return Ok(squares);
            }

            return Ok();
        }

    }
}