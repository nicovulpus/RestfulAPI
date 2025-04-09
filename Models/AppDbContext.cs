using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Models
{
    public class AppDbContext : DbContext
    {
        // Constructor accepting options (like connection string config)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets = Tables in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAddressLog> UserAddressLogs { get; set; }

        // Fluent API Configuration (optional but powerful)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CONFIGURING THE RELATIONSHIPS MANUALLY
            modelBuilder.Entity<UserAddressLog>()
                .HasOne(ual => ual.User)
                .WithMany(u => u.AddressLogs)
                .HasForeignKey(ual => ual.UserId);

            modelBuilder.Entity<UserAddressLog>()
                .HasOne(ual => ual.Address)
                .WithMany(a => a.UserVisits)
                .HasForeignKey(ual => ual.AddressId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.CurrentAddress)
                .WithMany()
                .HasForeignKey(u => u.CurrentAddressId);
        }
    }
}
