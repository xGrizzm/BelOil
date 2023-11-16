using Microsoft.EntityFrameworkCore;
using webapi.core.Entities;
using webapi.repositories.Contexts;
using webapi.repositories.contracts;

namespace webapi.repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(PostgreDbContext postgreDbContext) : base(postgreDbContext) { }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await Context.Users
                .Include(u => u.Fields)
                    .ThenInclude(f => f.OilPumps)
                .ToListAsync();
        }

        public async Task<UserEntity> GetAsync(string login)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<UserEntity> GetAsync(int userId)
        {
            return await Context.Users
                .Include(u => u.Fields)
                    .ThenInclude(f => f.OilPumps)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task UpdateBarrelsAsync(int userId, int barrels)
        {
            UserEntity user = await GetAsync(userId);

            user.Barrels = barrels;

            await Context.SaveChangesAsync();
        }
    }
}
