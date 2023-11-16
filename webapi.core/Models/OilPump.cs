namespace webapi.core.Models
{
    public class OilPump
    {
        public int Id { get; set; }

        public DateTime NextPumping { get; set; }

        public int FieldId { get; set; }

        public int Barrels { get; set; }
    }
}
