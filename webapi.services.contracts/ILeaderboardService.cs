using webapi.core.Models;

namespace webapi.services.contracts
{
    public interface ILeaderboardService
    {
        Task<IEnumerable<Leader>> GetLeaderboardAsync();
    }
}
