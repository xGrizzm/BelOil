using Microsoft.EntityFrameworkCore;
using webapi.core.Entities;
using webapi.repositories.Contexts;
using webapi.repositories.contracts;

namespace webapi.repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(PostgreDbContext postgreDbContext) : base(postgreDbContext) { }

        public async Task<UserEntity> GetAsync(string login)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}
