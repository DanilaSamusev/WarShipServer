namespace WarShipServer.Models
{
    public class Player
    {
        public bool IsSitFree { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsPlayerTurn { get; set; }
        public bool IsPlayerReady { get; set; }
        public string Name { get; set; }

        public Player(bool isPlayerTurn)
        {
            IsSitFree = true;
            IsLoggedIn = false;
            IsPlayerTurn = isPlayerTurn;
            IsPlayerReady = false;
            Name = "Unknown";
        }
    }
}