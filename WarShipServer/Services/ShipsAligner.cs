using System;
using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class ShipsAligner
    {
        public void PlantShipsRandom(Field field, Fleet fleet)
        {
            Random random = new Random();

            for (int i = 9; i >= 0;)
            {
                int point = random.Next(100);
                int directionNumber = random.Next(2);
                Direction direction = (Direction)directionNumber;

                PointsManager pointsManager = new PointsManager();
                PointsValidator validator = new PointsValidator();
                int[] points = pointsManager.GetSquaresNumbersForShipPlanting(fleet.Ships[i], point, direction);

                if (validator.ValidatePoints(field, points, direction))
                {
                    PlantShip(fleet.Ships[i], points, field);
                    field.ShipsOnField++;
                    i--;
                }
            }
        }

        public void PlantShip(Ship ship, int[] points, Field field)
        {
            for (int i = 0; i < points.Length; i++)
            {
                PlantDeck(ship.Decks[i], points[i], field, ship.Id);
            }
        }

        private void PlantDeck(Deck deck, int point, Field field, int shipNumber)
        {
            deck.Position = point;
            field.Squares[point].ShipNumber = shipNumber;
        }
        
        
        
    }
}