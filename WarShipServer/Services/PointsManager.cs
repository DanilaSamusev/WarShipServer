using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class PointsManager
    {
        public int[] GetSquaresNumbersForShipPlanting(Ship ship, int firstPoint, Direction direction)
        {
            int[] points = new int[ship.Decks.Length];
            points[0] = firstPoint;
            FillRequiredPoints(points, direction);

            return points;
        }
        
        public int[] JoinPoints(int[] possiblePoints, int[] previousPoints)
        {
            int[] changedPoints = new int[previousPoints.Length + possiblePoints.Length];
            possiblePoints.CopyTo(changedPoints, 0);
            previousPoints.CopyTo(changedPoints, possiblePoints.Length);

            return changedPoints;
        }
        
        private void FillRequiredPoints(int[] points, Direction direction)
        {
            int step = direction == Direction.Horizontally ? 1 : -10;

            for (int j = 1; j < points.Length; j++)
            {
                points[j] = points[j - 1] + step;
            }
        }
    }
}