using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class SquaresManager
    {
        public int[] LastCheckedSquaresNumbers { get; set; }
        private readonly PointsManager _pointsManager;

        public SquaresManager(PointsManager pointsManager)
        {
            _pointsManager = pointsManager;
        }

        public void SetIsChecked(Field field, int[] requiredPoints, bool condition)
        {
            foreach (var point in requiredPoints)
            {
                field.Squares[point].IsChecked = condition;
            }
        }

        public Square[] PaintSquaresForShipPlanting(Field field, int[] requiredSquaresNumbers)
        {
            if (LastCheckedSquaresNumbers != null)
            {
                SetIsChecked(field, LastCheckedSquaresNumbers, false);
            }

            SetIsChecked(field, requiredSquaresNumbers, true);

            var changedSquaresNumbers = LastCheckedSquaresNumbers == null ?
                requiredSquaresNumbers :
                _pointsManager.JoinPoints(requiredSquaresNumbers, LastCheckedSquaresNumbers);

            LastCheckedSquaresNumbers = requiredSquaresNumbers;

           return GetSquaresByNumbers(field, changedSquaresNumbers);
        }
        
        public Square[] GetSquaresByNumbers(Field field, int[] requiredPoints)
        {
            Square[] requiredSquares = new Square[requiredPoints.Length];

            for (int i = 0; i < requiredPoints.Length; i++)
            {
                requiredSquares[i] = field.Squares[requiredPoints[i]];
            }

            return requiredSquares;
        }
    }
}