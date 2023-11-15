using webapi.core.Entities;

namespace webapi.repositories.contracts
{
    public interface IOilPumpRepository
    {
        Task<OilPumpEntity> AddAsync(OilPumpEntity oilPump);

        Task<OilPumpEntity> GetAsync(int oilPumpId);

        Task UpdateNextPumpingAsync(int oilPumpId, DateTime nextPumping);
    }
}
