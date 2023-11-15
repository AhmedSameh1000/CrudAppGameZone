using GameZone.Seeding;

namespace GameZone.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>().HasKey(c => new { c.GameId, c.DeviceId });
            modelBuilder.Entity<Category>().HasData(CategorySeeding.LoadCategories());
            modelBuilder.Entity<Device>().HasData(DeviceSeeding.LoadDevices());
            base.OnModelCreating(modelBuilder);
        }
    }
}