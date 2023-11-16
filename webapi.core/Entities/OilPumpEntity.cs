namespace webapi.core.Entities
{
    public class OilPumpEntity : BaseEntity
    {
        public DateTimeOffset NextPumping { get; set; }

        public int FieldId {  get; set; }
        public FieldEntity Field { get; set; }

        public OilPumpEntity(int id, DateTimeOffset nextPumping, int fieldId) : base(id)
        {
            NextPumping = nextPumping;
            FieldId = fieldId;
        }

        public OilPumpEntity(DateTimeOffset nextPumping, int fieldId)
        {
            NextPumping = nextPumping;
            FieldId = fieldId;
        }
    }
}
