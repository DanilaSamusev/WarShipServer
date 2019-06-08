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
        private static int _shipsOnBoard;

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
            ShipsAligner shipsAligner = new ShipsAligner(PlayerField, Models.PlayerField.Fleet);

            if (_shipsOnBoard < 4)
            {
                int[] points = shipsAligner.GetPoints(Models.PlayerField.Fleet.Ships[0], square.Id, 0);

                foreach (int point in points)
                {
                    PlayerField.Squares[point].IsChecked = !PlayerField.Squares[point].IsChecked;
                }
            }

            return Ok(PlayerField.Squares[square.Id]);
        }


        public IActionResult HandleClick([FromBody] Square square)
        {
            if (_shipsOnBoard < 4)
            {
                PlayerField.Squares[square.Id].HasShip = true;
                _shipsOnBoard++;
            }

            return Ok(PlayerField);
        }
    }
}