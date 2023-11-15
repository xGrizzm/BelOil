using Microsoft.EntityFrameworkCore;
using webapi.core.Entities;
using webapi.repositories.Contexts;
using webapi.repositories.contracts;

namespace webapi.repositories
{
    public class FieldRepository : BaseRepository, IFieldRepository
    {
        public FieldRepository(PostgreDbContext postgreDbContext) : base(postgreDbContext) { }

        public async Task<FieldEntity> AddAsync(FieldEntity field)
        {
            FieldEntity addedField = (await Context.Fields.AddAsync(field)).Entity;
            await Context.SaveChangesAsync();
            return addedField;
        }

        public async Task<IEnumerable<FieldEntity>> GetAsync(int userId)
        {
            return await Context.Fields
                .Include(f => f.OilPumps)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
    }
}
