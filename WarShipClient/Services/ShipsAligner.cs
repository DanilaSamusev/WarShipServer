using System;
using WarShipClient.Models;

namespace WarShipClient.Services
{
    public class ShipsAligner
    {
        public void AlignShipsRandom(Field field)
        {
            Random random = new Random();

            for (int i = 9; i >= 0;)
            {
                int point = random.Next(100);
                int direction = random.Next(2);

                PossiblePointsCreature creature = new PossiblePointsCreature();
                PointsValidator validator = new PointsValidator();
                int[] points = creature.GetPossiblePoints(field.Fleet.Ships[i], point, direction);

                if (validator.ValidatePoints(field, points, direction))
                {
                    SetShip(field.Fleet.Ships[i], points, field);
                    field.Fleet.ShipsOnField++;
                    i--;
                }
            }
        }

        public void SetShip(Ship ship, int[] points, Field field)
        {
            for (int i = 0; i < points.Length; i++)
            {
                SetDeck(ship.Decks[i], points[i], field);
            }
        }

        private void SetDeck(Deck deck, int point, Field field)
        {
            deck.Position = point;
            field.Squares[point].HasShip = true;
        }
        
        
        
    }
}