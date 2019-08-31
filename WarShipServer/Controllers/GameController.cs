using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
            
            gameData.Players[0].IsLogged = true;
            gameData.Players[0].IsPlayerReady = true;
            gameData.Players[0].Name = "Computer";

            gameData.Players[1].IsLogged = true;
            
            return Ok(gameData);
        }

        [HttpGet("multiPlayer")]
        public  IActionResult GetMultiPlayerData([FromQuery] string playerName)
        {
            if (!GameData.Players[0].IsLogged)
            {
                GameData.Players[0].IsLogged = true;
                GameData.Players[0].Name = playerName;
                GameData.PlayerId = 0;
                GameData.EnemyId = 1;
            }
            else
            {
                GameData.Players[1].IsLogged = true;
                GameData.Players[1].Name = playerName;
                GameData.PlayerId = 1;
                GameData.EnemyId = 0;
            }
            
            return Ok(GameData);
        }
    }
    
}