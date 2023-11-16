using AutoMapper;
using Microsoft.AspNetCore.Http;
using webapi.core.Entities;
using webapi.core.Models;
using webapi.repositories.contracts;
using webapi.services.contracts;

namespace webapi.services
{
    public class LeaderboardService : BaseService, ILeaderboardService
    {
        private readonly IUserRepository _userRepository;

        public LeaderboardService(IUserRepository userRepository,

            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(httpContextAccessor, mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Leader>> GetLeaderboardAsync()
        {
            IEnumerable<UserEntity> users = await _userRepository.GetAllAsync();

            return users
                .Select(u => new Leader(u.Name, u.Fields.Count, u.Fields.SelectMany(f => f.OilPumps).Count(), u.Barrels))
                .OrderByDescending(l => l.TotalBarrels);
        }
    }
}
