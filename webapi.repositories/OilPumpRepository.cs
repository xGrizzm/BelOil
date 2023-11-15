using webapi.repositories.Contexts;
using webapi.repositories.contracts;

namespace webapi.repositories
{
    public class OilPumpRepository : BaseRepository, IOilPumpRepository
    {
        public OilPumpRepository(PostgreDbContext postgreDbContext) : base(postgreDbContext) { }
    }
}
