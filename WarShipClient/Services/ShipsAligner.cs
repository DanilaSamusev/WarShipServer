using System;
using WarShipClient.Models;

namespace WarShipClient.Services
{
    public class ShipsAligner
    {
        private static Field Field;
        private static Ship[] Ships;

        public ShipsAligner(Field field, Fleet fleet)
        {
            Field = field;
            Ships = fleet.Ships;
        }

        public void AlignShips()
        {
            Random random = new Random();

            for (int i = 0; i < 10;)
            {
                int point = random.Next(100);
                int direction = random.Next(2);
                int step = direction == 0 ? 1 : -10;

                int[] points = new int[Ships[i].Decks.Length];

                points[0] = point;

                for (int j = 1; j < points.Length; j++)
                {
                    points[j] = points[j - 1] + step;
                }

                if (CheckPoints(points, direction))
                {
                    for (int j = 0; j < points.Length; j++)
                    {
                        SetShip(Ships[i], points);
                    }

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
            Field.Squares[point].HasShip = true;
        }

        public static bool CheckPoints(int[] points, int direction)
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

        public static bool CheckPoint(int point)
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
                    if (Field.Squares[i].HasShip)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}