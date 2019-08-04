namespace WarShipServer.Models
{
    public class Ship
    {       
        public Deck[] Decks { get; set; }
        public int Id { get; set; }
        public bool IsAlive { get; set; }
        private int _hitsNumber;
        public int HitsNumber
        {
            get => _hitsNumber;
            set
            {
                _hitsNumber = value;
                if (_hitsNumber == Decks.Length)
                {
                    IsAlive = false;
                }
            }
        }
        
        public Ship(int decksCount, int id)
        {
            Decks = new Deck[decksCount];

            for (int i = 0; i < Decks.Length; i++)
            {
                Decks[i] = new Deck(0);
            }

            Id = id;
            IsAlive = true;
        }
        
    }
}