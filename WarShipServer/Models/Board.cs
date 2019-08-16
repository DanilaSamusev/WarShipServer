namespace WarShipServer.Models
{
    public class Board
    {
        public int Id { get; set; }
        public bool IsPlayerReady { get; set; }
        public Field Field { get; set; }
        public Fleet Fleet { get; set; }

        public Board(int id, bool isPlayerReady)
        {
            Id = id;
            IsPlayerReady = isPlayerReady;
            Field = new Field();
            Fleet = new Fleet();
        }
    }
}