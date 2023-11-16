namespace webapi.core.Models
{
    public class Leader
    {
        public string Name {  get; set; }

        public int FieldsCount { get; set; }

        public int OilPumpsCount { get; set; }

        public int TotalBarrels { get; set; }

        public Leader(string name, int fieldsCount, int oilPumpsCount, int totalBarrels)
        {
            Name = name;
            FieldsCount = fieldsCount;
            OilPumpsCount = oilPumpsCount;
            TotalBarrels = totalBarrels;
        }
    }
}
