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
        public static Field Field { get; set; }
        private static int _shipNumber;

        public PlayerFieldController()
        {
            Field = PlayerField.NewPlayerField();
        }

        [HttpGet]
        public IActionResult GetSquares()
        {
            return Ok(Field.Squares);
        }

        [HttpPut]
        public IActionResult HandleCursorOver([FromBody] Square checkedSquare)
        {                        
            Square[] checkedSquares = SetIsChecked(checkedSquare, true);         

            return Ok(checkedSquares);
        }

        [HttpPut("mouseOut")]
        public IActionResult HandleCursorOut([FromBody] Square checkedSquare)
        {           
            Square[] checkedSquares = SetIsChecked(checkedSquare, false);        
            
            return Ok(checkedSquares);
        }

        [HttpPut("setShip")]
        public IActionResult HandleClick([FromBody] Square clickedSquare)
        {
            PossiblePointsCreature creature = new PossiblePointsCreature();
            Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
            int[] points = creature.GetPossiblePoints(currentShip, clickedSquare.Id, 0);
            Square[] squares = new Square[currentShip.Decks.Length];

            PointsChecker checker = new PointsChecker(Field);
            ShipsAligner aligner = new ShipsAligner(Field, PlayerField.Fleet);

            if (checker.CheckPoints(points, 0))
            {
                aligner.SetShip(currentShip, points);
                _shipNumber++;
            }

            for (int i = 0; i < points.Length; i++)
            {
                squares[i] = Field.Squares[points[i]];
            }
            
           

            return Ok(squares);
        }

        private Square[] SetIsChecked(Square checkedSquare, bool isChecked)
        {           
                PossiblePointsCreature creature = new PossiblePointsCreature();
                Ship currentShip = PlayerField.Fleet.Ships[_shipNumber];
                int[] points = creature.GetPossiblePoints(currentShip, checkedSquare.Id, 0);

                Square[] checkedSquares = new Square[currentShip.Decks.Length];

                for (int i = 0; i < points.Length; i++)
                {
                    Field.Squares[points[i]].IsChecked = isChecked;
                    checkedSquares[i] = Field.Squares[points[i]];
                }

                return checkedSquares;          
        }
    }
}