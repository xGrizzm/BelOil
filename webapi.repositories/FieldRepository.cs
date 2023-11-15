using webapi.repositories.Contexts;
using webapi.repositories.contracts;

namespace webapi.repositories
{
    public class FieldRepository : BaseRepository, IFieldRepository
    {
        public FieldRepository(PostgreDbContext postgreDbContext) : base(postgreDbContext) { }
    }
}
