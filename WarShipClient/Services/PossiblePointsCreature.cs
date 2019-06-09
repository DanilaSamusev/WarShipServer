using WarShipClient.Models;

namespace WarShipClient.Services
{
    public class PossiblePointsCreature
    {
        public int[] GetPossiblePoints(Ship ship, int point, int direction)
        {
            int[] points = new int[ship.Decks.Length];
            points[0] = point;
            FillPoints(points, direction);

            return points;
        }

        public void FillPoints(int[] points, int direction)
        {
            int step = direction == 0 ? 1 : -10;

            for (int j = 1; j < points.Length; j++)
            {
                points[j] = points[j - 1] + step;
            }
        }
    }
}