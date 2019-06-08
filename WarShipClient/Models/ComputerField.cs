using WarShipClient.Services;

namespace WarShipClient.Models
{
    public class ComputerField : Field
    {
        private static ComputerField field;
        private static ShipsAligner _aligner;

        public static ComputerField NewComputerField()
        {
            if (field != null) return field;

            Fleet fleet = new Fleet();
            field = new ComputerField();
            _aligner = new ShipsAligner(field, fleet);
            _aligner.AlignShipsRandom();

            return field;
        }
    }
}