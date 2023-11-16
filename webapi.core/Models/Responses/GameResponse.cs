namespace webapi.core.Models.Responses
{
    public class GameResponse
    {
        public int Barrels {  get; set; }

        public IEnumerable<Field> Fields { get; set; }

        public int FieldPrice { get; set; }

        public GameResponse(int barrels, IEnumerable<Field> fields, int fieldPrice)
        {
            Barrels = barrels;
            Fields = fields;
            FieldPrice = fieldPrice;
        }
    }
}
