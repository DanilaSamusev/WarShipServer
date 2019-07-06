using System;
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

        public PlayerFieldController(Game game, PointsManager pointsManager,
            PointsValidator pointsValidator,
            SquaresManager squaresManager)
        {
            _game = game;
            _pointsManager = pointsManager;
            _pointsValidator = pointsValidator;
            _squaresManager = squaresManager;
        }

        [HttpPut("squaresForShipPlanting")]
        public IActionResult PaintSquaresForShipPlanting([FromQuery] int id, int direction)
        {
            if (_game.PlayerField.Fleet.ShipsOnField == 10)
            {
                return BadRequest();
            }

            Ship currentShip = _game.PlayerField.Fleet.Ships[_game.PlayerField.Fleet.ShipsOnField];
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
            if (_game.PlayerField.Fleet.ShipsOnField != 10)
            {
                Ship currentShip = _game.PlayerField.Fleet.Ships[_game.PlayerField.Fleet.ShipsOnField];
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

                    _game.PlayerField.Fleet.ShipsOnField++;
                    return Ok(requiredSquares);
                }
            }

            return BadRequest();
        }

        [HttpPut("computerShot")]
        public IActionResult MakeComputerShot()
        {
            Random random = new Random();
            int id;

            do
            {
                id = random.Next(100);
            } while (_game.PlayerField.Squares[id].IsClicked);

            _game.PlayerField.Squares[id].IsClicked = true;

            if (!_game.PlayerField.Squares[id].HasShip)
            {
                _game.IsPlayerTurn = true;
            }

            return Ok(_game.PlayerField.Squares[id]);
        }
    }
}