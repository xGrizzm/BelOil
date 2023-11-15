namespace webapi.core.Entities
{
    public class OilPumpEntity : BaseEntity
    {
        public DateTime NextPumping { get; set; }

        public int FieldId {  get; set; }
        public FieldEntity Field { get; set; }

        public OilPumpEntity(int id, DateTime nextPumping, int fieldId) : base(id)
        {
            NextPumping = nextPumping;
            FieldId = fieldId;
        }
    }
}
