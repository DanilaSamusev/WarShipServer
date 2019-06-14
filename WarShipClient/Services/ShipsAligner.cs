using System;
using WarShipClient.Models;

namespace WarShipClient.Services
{
    public class ShipsAligner
    {
        private readonly Field _field;
        private readonly Ship[] _ships;

        public ShipsAligner(Field field, Fleet fleet)
        {
            _field = field;
            _ships = fleet.Ships;
        }

        public void AlignShipsRandom()
        {
            Random random = new Random();

            for (int i = 0; i < 10;)
            {
                int point = random.Next(100);
                int direction = random.Next(2);

                PossiblePointsCreature creature = new PossiblePointsCreature();
                PointsValidator validator = new PointsValidator();
                int[] points = creature.GetPossiblePoints(_ships[i], point, direction);

                if (validator.ValidatePoints(_field, points, direction))
                {
                    SetShip(_ships[i], points);
                    i++;
                }
            }
        }

        public void SetShip(Ship ship, int[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                SetDeck(ship.Decks[i], points[i]);
            }
        }

        private void SetDeck(Deck deck, int point)
        {
            deck.Position = point;
            _field.Squares[point].HasShip = true;
        }
        
        
        
    }
}