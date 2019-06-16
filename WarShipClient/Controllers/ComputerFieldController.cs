using Microsoft.AspNetCore.Mvc;
using WarShipClient.Models;

namespace WarShipClient.Controllers
{    
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerFieldController : Controller
    {
        public static Field ComputerField { get; set; }

        public ComputerFieldController()
        {
            ComputerField = Models.ComputerField.NewComputerField();
        }
        
        [HttpGet]
        public IActionResult GetSquares()
        {
            return Ok(ComputerField.Squares);
        }
        
        [HttpPut]
        public IActionResult HandleClick([FromQuery] int id)
        {
            ComputerField.Squares[id].IsClicked = true;            
            return Ok(ComputerField.Squares[id]);
        }
    }
}