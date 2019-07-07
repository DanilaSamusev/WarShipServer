using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class PointsValidator
    {                            
        public bool ValidatePoints(Field field, int[] points, int direction)
        {
            int rowNumber = points[0] / 10;
            
            foreach (int point in points)
            {              
                if (point < 0 || point > 99 || !ValidatePoint(field, point))
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

        public bool ValidatePoint(Field field, int point)
        {
            int[] points = {
                point - 11, point - 10, point - 9,
                point - 1, point, point + 1,
                point + 9, point + 10, point + 11
            };
            
            if (point % 10 == 9)
            {
                points = new []
                {
                    point - 11, point - 10,
                    point - 1, point,
                    point + 9, point + 10
                };    
            }

            if (point % 10 == 0)
            {
                points = new []
                {
                    point - 10, point - 9,
                    point + 1, point,
                    point + 10, point + 11
                };    
            }
            
            foreach (int i in points)
            {                             
                if (i >= 0 && i < 100)
                {
                    if (field.Squares[i].ShipNumber > -1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}