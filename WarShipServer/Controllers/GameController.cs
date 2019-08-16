using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WarShipServer.Models;
using WarShipServer.Services;

namespace WarShipServer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly ShipsAligner _shipsAligner;
        private static GameData[] _gameData;
        public GameController(ShipsAligner shipsAligner)
        {
            _shipsAligner = shipsAligner;
            
        }

        [HttpGet("single")]
        public IActionResult GetSinglePlayerData()
        {
            GameData gameData = new GameData(0, 1, true,
                false, true);

            _shipsAligner.PlantShipsRandom(gameData.Boards[1].Field, gameData.Boards[1].Fleet);
            
            return Ok(gameData);
        }

        [HttpGet("multi")]
        public IActionResult GetMultiPlayerData()
        {

            if (_gameData == null)
            {

                _gameData = new[]
                {
                    new GameData(0, 1, true, false, false),
                    new GameData(1, 0, false, false, false)
                };
            }

            var gameData = _gameData[0].IsFree ? _gameData[0] : _gameData[1];

            gameData.IsFree = false;
            
            return Ok(gameData);
        }
    }
}