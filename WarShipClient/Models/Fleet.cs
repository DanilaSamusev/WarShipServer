using WarShipClient.Services;

namespace WarShipClient.Models
{
    public class Fleet
    {
        public string Condition { get; set; }
        public int LiveDecksCount { get; set; }
        public Ship[] Ships { get; set; }
        private readonly ShipFactory _shipFactory;
        
        public Fleet(ShipFactory factory, Field field)
        {
            _shipFactory = factory;
            Ships = _shipFactory.CreateShips(field.Squares);
            LiveDecksCount = 4;
            Condition = "Alive";
        }
        
        
    }
}