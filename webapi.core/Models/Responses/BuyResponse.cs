namespace webapi.core.Models.Responses
{
    public class BuyResponse<T> : BarrelsResponse
    {
        public T Item { get; set; }

        public BuyResponse(int barrels, T item) : base(barrels)
        {
            Item = item;
        }
    }
}
