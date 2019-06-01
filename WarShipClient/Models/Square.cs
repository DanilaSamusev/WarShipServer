namespace WarShipClient.Models
{
    public class Square
    {        
        public int Id { get; set; }
        public bool IsClicked { get; set; }
        public bool HasShip { get; set; }
        
        public Square(int id, bool isClicked)
        {
            Id = id;
            IsClicked = isClicked;
            HasShip = false;
        }
    }
}