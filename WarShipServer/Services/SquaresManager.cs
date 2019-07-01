using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class SquaresManager
    {       
        public void SetIsChecked(Field field, int[] requiredPoints, bool condition)
        {
            foreach (var point in requiredPoints)
            {
                field.Squares[point].IsChecked = condition;
            }
        }    

        public void SetHasShip()
        {
            
        }
        
        public Square[] GetSquaresByPoints(Field field, int[] requiredPoints)
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