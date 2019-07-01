using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class Game
    {
        public Field PlayerField { get; set; }
        public Field ComputerField { get; set; }
        public bool IsPlayerTurn { get; set; }
        private readonly ShootingManager _shootingManager;

        public Game(ShipsAligner shipsAligner, ShootingManager shootingManager)
        {
            PlayerField = new Field();
            ComputerField = new Field();
            shipsAligner.PlantShipsRandom(ComputerField);
            IsPlayerTurn = true;
            _shootingManager = shootingManager;
        }

        public Square[][] MakeShooting(int id)
        {
            Square[] computerSquares = _shootingManager.MakePlayerShot(ComputerField, id);
            Square[] playerSquares = null;

            if (!computerSquares[0].HasShip)
            {
                IsPlayerTurn = false;
                playerSquares = _shootingManager.MakeComputerShot(PlayerField);
                IsPlayerTurn = true;
            }
            
            return new[] {computerSquares, playerSquares};
        }
        
    }
}