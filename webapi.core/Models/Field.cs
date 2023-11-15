namespace webapi.core.Models
{
    public class Field
    {
        public int Id { get; set; }

        public IEnumerable<OilPump> OilPumps { get; set; }
    }
}
