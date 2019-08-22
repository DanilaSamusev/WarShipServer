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
        private static readonly GameData _gameData = new GameData();

        public GameController(ShipsAligner shipsAligner)
        {
            _shipsAligner = shipsAligner;
        }

        [HttpGet("single")]
        public IActionResult GetSinglePlayerData()
        {
            GameData gameData = new GameData {PlayerId = 1, EnemyId = 0, GameType = "Single player"};
            _shipsAligner.PlantShipsRandom(gameData.Boards[0].Field, gameData.Boards[0].Fleet);
            gameData.Players[0].IsPlayerReady = true;

            return Ok(gameData);
        }

        [HttpGet("multi")]
        public IActionResult GetMultiPlayerData()
        {
            
            if (_gameData.Players[0].IsSitFree)
            {
                _gameData.PlayerId = 0;
                _gameData.Players[0].IsSitFree = false;
                _gameData.EnemyId = 1;
            }
            else
            {
                _gameData.PlayerId = 1;
                _gameData.Players[1].IsSitFree = false;
                _gameData.EnemyId = 0;
            }
            
            return Ok(_gameData);
        }
    }
}