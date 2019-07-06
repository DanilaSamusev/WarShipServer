namespace WarShipServer.Models
{
    public class GameData
    {
        public Square[] PlayerSquares { get; set; }
        public Square[] ComputerSquares { get; set; }
        public bool IsPlayerTurn { get; set; }
        

        public GameData(Game game)
        {
            PlayerSquares = game.PlayerField.Squares;
            ComputerSquares = game.ComputerField.Squares;
            IsPlayerTurn = game.IsPlayerTurn;

        }
    }
}