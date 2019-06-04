using WarShipClient.Models;

namespace WarShipClient.Services
{
    public class ShipFactory
    {
        public Ship[] CreateShips(Square[] squares)
        {
            Ship[] ships =
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

            return ships;
        }          
    }
}