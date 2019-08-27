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
        private static readonly GameData GameData = new GameData();

        public GameController(ShipsAligner shipsAligner)
        {
            _shipsAligner = shipsAligner;
        }

        [HttpGet("singlePlayer")]
        public IActionResult GetSinglePlayerData()
        {
            GameData gameData = new GameData {PlayerId = 1, EnemyId = 0, GameType = "Single player"};
            _shipsAligner.PlantShipsRandom(gameData.Boards[0].Field, gameData.Boards[0].Fleet);
            gameData.Players[0].IsPlayerReady = true;
            gameData.Players[0].Name = "Computer";

            return Ok(gameData);
        }

        [HttpGet("multiPlayer")]
        public IActionResult GetMultiPlayerData()
        {
            
            if (GameData.Players[0].IsSitFree)
            {
                GameData.PlayerId = 0;
                GameData.Players[0].IsSitFree = false;
                GameData.EnemyId = 1;
            }
            else
            {
                GameData.PlayerId = 1;
                GameData.Players[1].IsSitFree = false;
                GameData.EnemyId = 0;
            }
            
            return Ok(GameData);
        }
    }
}