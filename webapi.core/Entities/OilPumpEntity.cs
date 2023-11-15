namespace webapi.core.Entities
{
    public class OilPumpEntity : BaseEntity
    {
        public DateTime NextPumping { get; set; }

        public int FieldId {  get; set; }
        public FieldEntity Field { get; set; }
    }
}
