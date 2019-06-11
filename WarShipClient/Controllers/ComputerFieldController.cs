using Microsoft.AspNetCore.Mvc;
using WarShipClient.Models;

namespace WarShipClient.Controllers
{    
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerFieldController : Controller
    {
        public static Field Field { get; set; }

        public ComputerFieldController()
        {
            Field = ComputerField.NewComputerField();
        }
        
        [HttpGet]
        public IActionResult GetSquares()
        {
            return Ok(Field.Squares);
        }
        
        [HttpPut]
        public IActionResult HandleClick([FromBody] Square square)
        {
            Field.Squares[square.Id].IsClicked = true;            
            return Ok(Field.Squares[square.Id]);
        }
    }
}