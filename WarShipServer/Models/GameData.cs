namespace WarShipServer.Models
{
    public class GameData
    {
        public Board[] Boards { get; set; }
        public Player[] Players { get; set; }
        public string GameType { get; set; }
        public int PlayerId { get; set; }
        public int EnemyId { get; set; }
        public string Events { get; set; }

        public GameData()
        {
            Boards = new[]
            {
                new Board(0),
                new Board(1)
            };

            Players = new[]
            {
                new Player(false),
                new Player(true)
            };

            Events = "";
        }
    }
}