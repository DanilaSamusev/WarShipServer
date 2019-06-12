using WarShipClient.Models;

namespace WarShipClient.Services
{
    public class PointsValidator
    {
        private Field Field;
        
        public PointsValidator(Field field)
        {
            Field = field;
        } 
        
        public bool ValidatePoints(int[] points, int direction)
        {
            int rowNumber = points[0] / 10;
            
            foreach (int point in points)
            {
                
                if (point < 0 || point > 99 || !ValidatePoint(point))
                {
                    return false;
                }
            }

            if (direction == 0)
            {                

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

        public bool ValidatePoint(int point)
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