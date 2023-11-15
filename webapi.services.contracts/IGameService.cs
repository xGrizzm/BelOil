using webapi.core.Models;
using webapi.core.Models.Responses;

namespace webapi.services.contracts
{
    public interface IGameService
    {
        Task<GameResponse> GetGameAsync();

        Task<CollectOilPumpResponse> CollectOilPumpAsync(int fieldId, int oilPumpId);

        Task<BuyResponse<Field>> BuyFieldAsync();

        Task<BuyResponse<OilPump>> BuyOilPumpAsync(int fieldId);
    }
}
