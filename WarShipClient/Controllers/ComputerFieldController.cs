using Microsoft.AspNetCore.Mvc;
using WarShipClient.Models;

namespace WarShipClient.Controllers
{    
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerFieldController : Controller
    {
        private static Field ComputerField { get; set; }

        public ComputerFieldController()
        {
            ComputerField = Models.ComputerField.NewComputerField();
        }
        
        [HttpGet]
        public IActionResult GetField()
        {
            return Ok(ComputerField);
        }
        
        [HttpPut]
        public IActionResult UpdateField([FromBody] Square square)
        {
            ComputerField.Squares[square.Id].IsClicked = true;            
            return Ok(ComputerField.Squares[square.Id]);
        }
    }
}