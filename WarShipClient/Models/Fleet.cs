using WarShipClient.Services;

namespace WarShipClient.Models
{
    public class Fleet
    {
        public string Condition { get; set; }
        public int LiveDecksCount { get; set; }
        public Ship[] Ships { get; set; }       
        private readonly ShipsAligner _aligner;
        
        public Fleet(ShipsAligner aligner)
        {         
            _aligner = aligner;
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
            LiveDecksCount = 4;
            Condition = "Alive";
            _aligner.AlignShips(Ships);
        }
        
        
    }
}