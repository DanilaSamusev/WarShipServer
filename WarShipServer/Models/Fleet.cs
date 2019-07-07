using WarShipServer.Services;

namespace WarShipServer.Models
{
    public class Fleet
    {
        public int ShipsOnField { get; set; }
        public Ship[] Ships { get; set; }

        public Fleet()
        {
            Ships = new[]
            {
                new Ship(1, 0),
                new Ship(1, 1),
                new Ship(1, 2),
                new Ship(1, 3),
                new Ship(2, 4),
                new Ship(2, 5),
                new Ship(2, 6),
                new Ship(3, 7),
                new Ship(3, 8),
                new Ship(4, 9)
            };

            ShipsOnField = 0;
        }
    }
}