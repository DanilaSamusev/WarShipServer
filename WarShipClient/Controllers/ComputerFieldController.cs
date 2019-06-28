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
        public static Field ComputerField { get; set; }
        private readonly ShipsAligner _shipsAligner;

        public ComputerFieldController(ShipsAligner shipsAligner)
        {
            _shipsAligner = shipsAligner;
        }

        [HttpGet]
        public IActionResult GetFieldData()
        {
            if (ComputerField == null)
            {
                ComputerField = new Field();
                _shipsAligner.PlantShipsRandom(ComputerField);
            }

            return Ok(ComputerField.Squares);
        }

        [HttpPut("makePlayerShot")]
        public IActionResult MakePlayerShot([FromQuery] int id)
        {
            ComputerField.Squares[id].IsClicked = true;
            return Ok(ComputerField.Squares[id]);
        }
    }
}