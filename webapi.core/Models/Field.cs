namespace webapi.core.Models
{
    public class Field
    {
        public int Id { get; set; }

        public int Multiplier { get; set; }

        public IEnumerable<OilPump> OilPumps { get; set; }

        public int OilPumpPrice { get; set; }
    }
}
