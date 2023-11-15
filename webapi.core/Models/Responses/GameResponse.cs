namespace webapi.core.Models.Responses
{
    public class GameResponse
    {
        public IEnumerable<Field> Fields { get; set; }

        public GameResponse(IEnumerable<Field> fields)
        {
            Fields = fields;
        }
    }
}
