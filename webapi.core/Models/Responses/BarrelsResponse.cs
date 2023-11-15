namespace webapi.core.Models.Responses
{
    public class BarrelsResponse
    {
        public int Barrels { get; set; }

        public BarrelsResponse(int barrels)
        {
            Barrels = barrels;
        }
    }
}
