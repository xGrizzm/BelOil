using webapi.repositories.Contexts;

namespace webapi.repositories
{
    public abstract class BaseRepository
    {
        private PostgreDbContext _context;
        public PostgreDbContext Context => _context;

        public BaseRepository(PostgreDbContext postgreDbContext)
        {
            _context = postgreDbContext;
        }
    }
}
