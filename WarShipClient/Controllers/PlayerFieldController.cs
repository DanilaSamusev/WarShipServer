using Microsoft.AspNetCore.Mvc;
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
        private static int _shipNumber = 4;

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
            Square[] squares = new Square[Models.PlayerField.Fleet.Ships[_shipNumber].Decks.Length];
            int[] points = shipsAligner.GetPoints(Models.PlayerField.Fleet.Ships[_shipNumber], square.Id, 0);

            for (int i = 0; i < points.Length; i++)
            {
                PlayerField.Squares[points[i]].IsChecked = !PlayerField.Squares[points[i]].IsChecked;
                squares[i] = PlayerField.Squares[points[i]];
            }

            return Ok(squares);
        }


        public IActionResult HandleClick([FromBody] Square square)
        {
            

            return Ok(PlayerField);
        }
    }
}