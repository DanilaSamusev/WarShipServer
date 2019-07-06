using System;
using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class ShipsAligner
    {
        public void PlantShipsRandom(Field field)
        {
            Random random = new Random();

            for (int i = 9; i >= 0;)
            {
                int point = random.Next(100);
                int direction = random.Next(2);

                PointsManager pointsManager = new PointsManager();
                PointsValidator validator = new PointsValidator();
                int[] points = pointsManager.GetSquaresNumbersForShipPlanting(field.Fleet.Ships[i], point, direction);

                if (validator.ValidatePoints(field, points, direction))
                {
                    PlantShip(field.Fleet.Ships[i], points, field);
                    field.Fleet.ShipsOnField++;
                    i--;
                }
            }
        }

        public void PlantShip(Ship ship, int[] points, Field field)
        {
            for (int i = 0; i < points.Length; i++)
            {
                PlantDeck(ship.Decks[i], points[i], field);
            }
        }

        private void PlantDeck(Deck deck, int point, Field field)
        {
            deck.Position = point;
            field.Squares[point].HasShip = true;
        }
        
        
        
    }
}