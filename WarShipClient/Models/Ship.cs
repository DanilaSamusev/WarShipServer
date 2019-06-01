namespace WarShipClient.Models
{
    public class Ship
    {
        public string Condition { get; set; }        
        public Deck[] Decks { get; set; }
        
        public Ship(string condition, Deck[] decks)
        {
            Condition = condition;
            Decks = decks;
        }
        
    }
}