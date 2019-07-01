namespace WarShipServer.Services
{
    public class PointsManager
    {
        public int[] JoinPoints(int[] possiblePoints, int[] previousPoints)
        {
            int[] changedPoints = new int[previousPoints.Length + possiblePoints.Length];
            possiblePoints.CopyTo(changedPoints, 0);
            previousPoints.CopyTo(changedPoints, possiblePoints.Length);

            return changedPoints;
        }
    }
}