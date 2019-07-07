namespace WarShipServer.Models
{
    public class Square
    {        
        public int Id { get; set; }
        public bool IsClicked { get; set; }
        public bool IsChecked { get; set; }
        public int ShipNumber { get; set; }
        
        public Square(int id, bool isClicked)
        {
            Id = id;
            IsClicked = isClicked;
            IsChecked = false;
            ShipNumber = -1;
        }
    }
}