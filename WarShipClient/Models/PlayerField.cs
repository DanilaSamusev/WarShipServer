namespace WarShipClient.Models
{
    public class PlayerField : Field
    {
        private static PlayerField field;        

        public static PlayerField NewPlayerField()
        {
            if (field != null) return field;
            
            field = new PlayerField();
            
            return field;
        }        
    }
}