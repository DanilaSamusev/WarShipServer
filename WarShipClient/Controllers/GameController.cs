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
        private static Field ComputerField { get; set; }
        private static Field PlayerField { get; set; }
        private static Fleet Fleet { get; set; }
        
        public GameController(Field field, Fleet fleet)
        {           
            Fleet = fleet;
            ComputerField = Models.ComputerField.NewComputerField(Fleet);
            PlayerField =  new Field(); 
        }
              
        [HttpGet]
        public IActionResult GetField()
        {          
            return Ok(new {ComputerField, PlayerField});
        }

        [HttpPut]
        public IActionResult UpdateSquare([FromBody] Square square)
        {
            ComputerField.Squares[square.Id].IsClicked = true;
            Console.WriteLine(ComputerField.Squares[square.Id].IsClicked);
            return Ok(ComputerField.Squares[square.Id]);
        }
    }
}