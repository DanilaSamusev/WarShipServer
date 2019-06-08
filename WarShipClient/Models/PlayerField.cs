namespace WarShipClient.Models
{
    public class PlayerField : Field
    {
        private static PlayerField _field;
        public static Fleet Fleet { get; set; }

        public static PlayerField NewPlayerField()
        {
            if (_field != null) return _field;
            
            Fleet = new Fleet();
            _field = new PlayerField();
            
            return _field;
        }        
    }
}