using WarShipClient.Services;

namespace WarShipClient.Models
{
    public class ComputerField : Field
    {
        private static ComputerField _field;
        private static Fleet _fleet;
        private static ShipsAligner _aligner;

        public static ComputerField NewComputerField()
        {
            if (_field != null) return _field;

            _fleet = new Fleet();
            _field = new ComputerField();
            _aligner = new ShipsAligner(_field, _fleet);
            _aligner.AlignShipsRandom();

            return _field;
        }
    }
}