using WarShipClient.Models;

namespace WarShipClient.Services
{
    public class PointsChecker
    {
        private Field Field;
        
        public PointsChecker(Field field)
        {
            Field = field;
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