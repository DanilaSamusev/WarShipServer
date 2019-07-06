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
        private readonly PossiblePointsCreature _possiblePointsCreature;
        private readonly PointsValidator _pointsValidator;
        private readonly SquaresManager _squaresManager;

        public PlayerFieldController(Game game, PossiblePointsCreature possiblePointsCreature,
            PointsValidator pointsValidator,
            SquaresManager squaresManager)
        {
            _game = game;
            _possiblePointsCreature = possiblePointsCreature;
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
            int[] requiredSquaresNumbers = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);
            
            if (_pointsValidator.ValidatePoints(_game.PlayerField, requiredSquaresNumbers, direction))
            {
                return Ok(_squaresManager.PaintSquaresForShipPlanting(_game.PlayerField, requiredSquaresNumbers));
            }

            _squaresManager.SetIsChecked(_game.PlayerField, _squaresManager.LastCheckedSquaresNumbers, false);
            return Ok(_squaresManager.GetSquaresByPoints(_game.PlayerField, _squaresManager.LastCheckedSquaresNumbers));
        }

        [HttpPut("plantShip")]
        public IActionResult PlantShip([FromQuery] int id, int direction)
        {
            if (_game.PlayerField.Fleet.ShipsOnField == 10)
            {
                return Ok();
            }

            Ship currentShip = _game.PlayerField.Fleet.Ships[_game.PlayerField.Fleet.ShipsOnField];
            int[] points = _possiblePointsCreature.GetPossiblePoints(currentShip, id, direction);
            Square[] squares = new Square[currentShip.Decks.Length];

            ShipsAligner aligner = new ShipsAligner();

            if (_pointsValidator.ValidatePoints(_game.PlayerField, points, direction))
            {
                aligner.PlantShip(currentShip, points, _game.PlayerField);

                for (int i = 0; i < points.Length; i++)
                {
                    squares[i] = _game.PlayerField.Squares[points[i]];
                }

                _game.PlayerField.Fleet.ShipsOnField++;
                return Ok(squares);
            }

            return Ok();
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