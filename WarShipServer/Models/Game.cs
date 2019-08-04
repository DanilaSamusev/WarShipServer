using WarShipServer.Services;

namespace WarShipServer.Models
{
    public class Game
    {
        public Field PlayerField { get; set; }
        public Field ComputerField { get; set; }
        public Fleet PlayerFleet { get; set; }
        public Fleet ComputerFleet { get; set; }
        public bool IsPlayerTurn { get; set; }
        public string WinnerName { get; set; }
        public string GameState { get; set; }
        

        public Game(ShipsAligner shipsAligner)
        {
            PlayerField = new Field();
            ComputerField = new Field();
            PlayerFleet = new Fleet();
            ComputerFleet = new Fleet();
            shipsAligner.PlantShipsRandom(ComputerField, ComputerFleet); 
            shipsAligner.PlantShipsRandom(PlayerField, PlayerFleet);
            IsPlayerTurn = true;
            WinnerName = null;
            GameState = "battle preparation";
        }

        
        
    }
}