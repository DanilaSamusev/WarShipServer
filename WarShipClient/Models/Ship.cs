namespace WarShipClient.Models
{
    public class Ship
    {       
        public Deck[] Decks { get; set; }
        
        public Ship(int decksCount)
        {
            Decks = new Deck[decksCount];

            for (int i = 0; i < Decks.Length; i++)
            {
                Decks[i] = new Deck(0);
            }           
        }
        
    }
}