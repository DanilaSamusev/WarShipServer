namespace WarShipServer.Models
{
    public class Field
    {
        public Square[] Squares { get; set; }
        public Field()
        {
            Squares = CreateSquares();
        }

        private Square[] CreateSquares()
        {
            Square[] squares = new Square[100];

            for (int i = 0; i < 100; i++)
            {
                squares[i] = new Square(i, false);
            }

            return squares;
        }
    }
}