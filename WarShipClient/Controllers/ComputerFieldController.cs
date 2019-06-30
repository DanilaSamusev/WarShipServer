using System;
using Microsoft.AspNetCore.Mvc;
using WarShipClient.Models;
using WarShipClient.Services;

namespace WarShipClient.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerFieldController : Controller
    {
        private readonly Game _game;

        public ComputerFieldController(Game game)
        {
            _game = game;
        }

        [HttpGet]
        public IActionResult GetFieldData()
        {
            Field computerField = _game.ComputerField;

            if (computerField != null)
            {
                return Ok(computerField.Squares);
            }

            return NotFound("Computer field is not found");
        }

        [HttpPut("makePlayerShot")]
        public IActionResult MakeShooting([FromQuery] int id)
        {
            return Ok(_game.MakeShooting(id));
        }

        
    }
}