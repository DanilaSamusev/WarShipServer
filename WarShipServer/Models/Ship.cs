namespace WarShipServer.Models
{
    public class Ship
    {       
        public Deck[] Decks { get; set; }
        public int Id { get; set; }
        public bool IsAlive { get; set; }
        public int HitsNumber { get; set; }
        
        public Ship(int decksCount, int id)
        {
            Decks = new Deck[decksCount];

            for (int i = 0; i < Decks.Length; i++)
            {
                Decks[i] = new Deck(0);
            }

            Id = id;
            IsAlive = true;
            HitsNumber = 0;
        }
        
    }
}