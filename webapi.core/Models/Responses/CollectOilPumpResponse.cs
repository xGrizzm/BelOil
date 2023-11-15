namespace webapi.core.Models.Responses
{
    public class CollectOilPumpResponse : BarrelsResponse
    {
        public DateTime NextPumping { get; set; }

        public CollectOilPumpResponse(int barrels, DateTime nextPumping) : base(barrels)
        {
            NextPumping = nextPumping;
        }
    }
}
