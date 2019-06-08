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

                if (SetShip(_ships[i], point, direction))
                {
                    i++;
                }
            }
        }

        public int[] GetPoints(Ship ship, int point, int direction)
        {
            int[] points = new int[ship.Decks.Length];
            points[0] = point;
            FillPoints(points, direction);

            return points;
        }
        
        public bool SetShip(Ship ship, int point, int direction)
        {
            int[] points = GetPoints(ship, point, direction);
            
            if (CheckPoints(points, direction))
            {              
                    for (int i = 0; i < points.Length; i++)
                    {
                        SetDeck(ship.Decks[i], points[i]);
                    }

                    return true;           
            }

            return false;
        }

        public void FillPoints(int[] points, int direction)
        {
            int step = direction == 0 ? 1 : -10;

            for (int j = 1; j < points.Length; j++)
            {
                points[j] = points[j - 1] + step;
            }
        }

        private void SetDeck(Deck deck, int point)
        {
            deck.Position = point;
            _field.Squares[point].HasShip = true;
        }

        public bool CheckPoints(int[] points, int direction)
        {
            foreach (int point in points)
            {
                if (point < 0 || point > 99 || !CheckPoint(point))
                {
                    return false;
                }
            }

            if (direction == 0)
            {
                int rowNumber = points[0] / 10;

                foreach (int point in points)
                {
                    if (point / 10 != rowNumber)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CheckPoint(int point)
        {
            int[] points =
            {
                point - 11, point - 10, point - 9,
                point - 1, point + 1,
                point + 9, point + 10, point + 11
            };

            foreach (int i in points)
            {
                if (i >= 0 && i < 100)
                {
                    if (_field.Squares[i].HasShip)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}