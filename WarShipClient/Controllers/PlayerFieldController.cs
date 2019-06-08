using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WarShipClient.Models;
using WarShipClient.Services;

namespace WarShipClient.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerFieldController : Controller
    {
        private static Field PlayerField { get; set; }
        private static int _decksOnBoard;

        public PlayerFieldController()
        {
            PlayerField = Models.PlayerField.NewPlayerField();
        }

        [HttpGet]
        public IActionResult GetField()
        {
            return Ok(PlayerField);
        }

        [HttpPut]
        public IActionResult UpdateField([FromBody] Square square)
        {
            PlayerField.Squares[square.Id].IsChecked = !PlayerField.Squares[square.Id].IsChecked;

            return Ok(PlayerField.Squares[square.Id]);
        }

        
        public IActionResult HandleClick([FromBody] Square square)
        {
            if (_decksOnBoard < 4)
            {
                PlayerField.Squares[square.Id].HasShip = true;
                _decksOnBoard++;
            }

            return Ok(PlayerField);
        }
    }
}