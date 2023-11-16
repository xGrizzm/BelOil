using Microsoft.EntityFrameworkCore;
using webapi.core.Entities;
using webapi.repositories.Contexts;
using webapi.repositories.contracts;

namespace webapi.repositories
{
    public class OilPumpRepository : BaseRepository, IOilPumpRepository
    {
        public OilPumpRepository(PostgreDbContext postgreDbContext) : base(postgreDbContext) { }

        public async Task<OilPumpEntity> AddAsync(OilPumpEntity oilPump)
        {
            OilPumpEntity addedOilPump = (await Context.OilPumps.AddAsync(oilPump)).Entity;
            await Context.SaveChangesAsync();
            return addedOilPump;
        }

        public async Task<OilPumpEntity> GetAsync(int oilPumpId)
        {
            return await Context.OilPumps
                .Include(op => op.Field)
                .FirstOrDefaultAsync(op => op.Id == oilPumpId);
        }

        public async Task UpdateNextPumpingAsync(int oilPumpId, DateTimeOffset nextPumping)
        {
            OilPumpEntity oilPumpEntity = await GetAsync(oilPumpId);

            oilPumpEntity.NextPumping = nextPumping;

            await Context.SaveChangesAsync();
        }
    }
}
