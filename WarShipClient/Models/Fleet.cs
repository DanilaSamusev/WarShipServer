using WarShipClient.Services;

namespace WarShipClient.Models
{
    public class Fleet
    {
        public int ShipsOnField { get; set; }
        public Ship[] Ships { get; set; }       
                
        public Fleet()
        {                   
            Ships = new[]
            {
                new Ship(1),
                new Ship(1),
                new Ship(1),
                new Ship(1),
                new Ship(2),
                new Ship(2),
                new Ship(2),
                new Ship(3),
                new Ship(3),
                new Ship(4)
            };

            ShipsOnField = 0;
        }
        
        
    }
}