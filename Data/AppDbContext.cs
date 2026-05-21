using Microsoft.EntityFrameworkCore;
using WebhookAPI.Models;

namespace WebhookAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<VehiculoWebhookQueue> VehiculoWebhookQueue { get; set; }

        public DbSet<Vehiculo> Vehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehiculoWebhookQueue>()
                .HasIndex(x => x.Id)
                .IsUnique();

            modelBuilder.Entity<Vehiculo>()
                .HasIndex(x => x.Id)
                .IsUnique();
        }
    }
}
