using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class Game
    {
        public Field PlayerField { get; set; }
        public Field ComputerField { get; set; }
        public bool IsPlayerTurn { get; set; }
        

        public Game(ShipsAligner shipsAligner)
        {
            PlayerField = new Field();
            ComputerField = new Field();
            shipsAligner.PlantShipsRandom(ComputerField);
            IsPlayerTurn = true;
        }

        
        
    }
}