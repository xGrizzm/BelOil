using Microsoft.EntityFrameworkCore;
using webapi.core.Entities;

namespace webapi.repositories.Contexts
{
    public class PostgreDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<FieldEntity> Fields { get; set; }

        public DbSet<OilPumpEntity> OilPumps { get; set; }

        public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<UserEntity> users = new List<UserEntity>
            {
                new UserEntity(1, "Нефтяник Василий", "vasil", "TXvkIvTwAkOlf0z9DmMSi5of4LsGk8XdtNKg7sSYt5Q=DwC06O3oIQSmbcTjGROkgVCGh1SY"),
                new UserEntity(2, "Чернозолотой магнат", "magnat", "LPWCorQonbIL4cTBMokHud+HZxeiPNYy0RUN38n+JXE=NNdS1Ouit8KQgN3g14dlLtMi5VYO"),
                new UserEntity(3, "Oil one love <3", "oillove", "NEcuTLnLwOfGMJZiYe1U9j4dvYl2vYw2vwnBIrPmABU=PycrDcC22bqNsuruIf0t0Yk9saXd")
            };
            modelBuilder.Entity<UserEntity>().HasData(users);

            List<FieldEntity> fields = new List<FieldEntity>
            {
                new FieldEntity(1, 1, 1),
                new FieldEntity(2, 1, 2),
                new FieldEntity(3, 1, 3)
            };
            modelBuilder.Entity<FieldEntity>().HasData(fields);

            List<OilPumpEntity> oilPumps = new List<OilPumpEntity>
            {
                new OilPumpEntity(1, DateTime.UtcNow, 1),
                new OilPumpEntity(2, DateTime.UtcNow, 2),
                new OilPumpEntity(3, DateTime.UtcNow, 3)
            };
            modelBuilder.Entity<OilPumpEntity>().HasData(oilPumps);
        }
    }
}