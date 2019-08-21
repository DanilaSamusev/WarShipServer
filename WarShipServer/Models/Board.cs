namespace WarShipServer.Models
{
    public class Board
    {
        public int Id { get; set; }
        public Field Field { get; set; }
        public Fleet Fleet { get; set; }

        public Board(int id)
        {
            Id = id;
            Field = new Field();
            Fleet = new Fleet();
            
        }
    }
}