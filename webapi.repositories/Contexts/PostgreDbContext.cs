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
                new UserEntity(1, "Нефтяник Василий", "vasil", "vasil"),
                new UserEntity(2, "Чернозолотой магнат", "magnat", "magnat"),
                new UserEntity(3, "Oil one love <3", "oillove", "oillove")
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
                new OilPumpEntity(1, DateTime.Now, 1),
                new OilPumpEntity(2, DateTime.Now, 2),
                new OilPumpEntity(3, DateTime.Now, 3)
            };
            modelBuilder.Entity<OilPumpEntity>().HasData(oilPumps);
        }
    }
}