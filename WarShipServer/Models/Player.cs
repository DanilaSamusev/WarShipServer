namespace WarShipServer.Models
{
    public class Player
    {
        public bool IsLogged { get; set; }
        public bool IsPlayerTurn { get; set; }
        public bool IsPlayerReady { get; set; }
        public string Name { get; set; }

        public Player(bool isPlayerTurn)
        {
            IsLogged = false;
            IsPlayerTurn = isPlayerTurn;
            IsPlayerReady = false;
            Name = "";
        }
    }
}