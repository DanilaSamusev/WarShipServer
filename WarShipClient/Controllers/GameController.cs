using System;
using Microsoft.AspNetCore.Mvc;
using WarShipClient.Models;
using WarShipClient.Services;

namespace WarShipClient.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {        
        private static Field Field { get; set; }
        private static Fleet Fleet { get; set; }
        
        public GameController(Field field, Fleet fleet)
        {            
            Field = field;
            Fleet = fleet;
        }
              
        [HttpGet]
        public IActionResult GetField()
        {          
            return Ok(Field);
        }

        [HttpPut]
        public IActionResult UpdateSquare([FromBody] Square square)
        {
            Field.Squares[square.Id].IsClicked = true;
            Console.WriteLine(Field.Squares[square.Id].IsClicked);
            return Ok(Field.Squares[square.Id]);
        }
    }
}