using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WarShipClient.Models;

namespace WarShipClient.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerFieldController : Controller
    {
        private static Field PlayerField { get; set; }
        
        public PlayerFieldController()
        {
            PlayerField = Models.PlayerField.NewPlayerField();
        }
        
        [HttpGet]
        public IActionResult GetField()
        {
            return Ok(PlayerField);
        }
    }
}