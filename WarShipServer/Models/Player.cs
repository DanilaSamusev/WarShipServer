namespace WarShipServer.Models
{
    public class Player
    {
        public bool IsPlayerTurn { get; set; }
        public bool IsPlayerReady { get; set; }
        public bool IsSitFree { get; set; }

        public Player(bool isPlayerTurn)
        {
            IsPlayerTurn = isPlayerTurn;
            IsPlayerReady = false;
            IsSitFree = true;
        }
    }
}