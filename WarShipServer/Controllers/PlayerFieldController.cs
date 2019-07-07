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
        private readonly PointsManager _pointsManager;
        private readonly PointsValidator _pointsValidator;
        private readonly SquaresManager _squaresManager;
        private readonly ShootingManager _shootingManager;

        public PlayerFieldController(Game game, PointsManager pointsManager,
            PointsValidator pointsValidator, SquaresManager squaresManager,
            ShootingManager shootingManager)
        {
            _game = game;
            _pointsManager = pointsManager;
            _pointsValidator = pointsValidator;
            _squaresManager = squaresManager;
            _shootingManager = shootingManager;
        }

        [HttpPut("squaresForShipPlanting")]
        public IActionResult PaintSquaresForShipPlanting([FromQuery] int id, int direction)
        {
            if (_game.PlayerFleet.ShipsOnField == 10)
            {
                return BadRequest();
            }

            Ship currentShip = _game.PlayerFleet.Ships[_game.PlayerFleet.ShipsOnField];
            int[] requiredSquaresNumbers = _pointsManager.GetSquaresNumbersForShipPlanting(currentShip, id,
                direction);

            if (_pointsValidator.ValidatePoints(_game.PlayerField, requiredSquaresNumbers, direction))
            {
                return Ok(_squaresManager.PaintSquaresForShipPlanting(_game.PlayerField, requiredSquaresNumbers));
            }

            _squaresManager.SetIsChecked(_game.PlayerField, _squaresManager.LastCheckedSquaresNumbers, false);
            return Ok(_squaresManager.GetSquaresByNumbers(_game.PlayerField, _squaresManager.LastCheckedSquaresNumbers));
        }

        [HttpPut("plantShip")]
        public IActionResult PlantShip([FromQuery] int id, int direction)
        {
            if (_game.PlayerFleet.ShipsOnField != 10)
            {
                Ship currentShip = _game.PlayerFleet.Ships[_game.PlayerFleet.ShipsOnField];
                int[] requiredSquaresNumbers = _pointsManager.GetSquaresNumbersForShipPlanting(currentShip, id,
                    direction);
                Square[] requiredSquares = new Square[currentShip.Decks.Length];

                ShipsAligner aligner = new ShipsAligner();

                if (_pointsValidator.ValidatePoints(_game.PlayerField, requiredSquaresNumbers, direction))
                {
                    aligner.PlantShip(currentShip, requiredSquaresNumbers, _game.PlayerField);

                    for (int i = 0; i < requiredSquaresNumbers.Length; i++)
                    {
                        requiredSquares[i] = _game.PlayerField.Squares[requiredSquaresNumbers[i]];
                    }

                    _game.PlayerFleet.ShipsOnField++;
                    return Ok(requiredSquares);
                }
            }

            return BadRequest();
        }

        [HttpPut("computerShot")]
        public IActionResult MakeComputerShot()
        {
            Square shotSquare = _shootingManager.MakeComputerShot();
            
            return Ok(shotSquare);
        }
    }
}