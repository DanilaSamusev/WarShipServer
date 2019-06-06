using WarShipClient.Services;

namespace WarShipClient.Models
{
    public class ComputerField : Field
    {
        private static ComputerField field;
        private static ShipsAligner _aligner;
        
        public static ComputerField NewComputerField(Fleet fleet)
        {
            if (field == null)
            {                
                field = new ComputerField();   
                _aligner = new ShipsAligner(field, fleet);
                _aligner.AlignShips();
            }

            return field;
        }
               
        
    }
}