using Microsoft.EntityFrameworkCore;
using HR_API.Models;

namespace HR_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Các bảng
        public DbSet<Device> Devices { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Devices_Phone_QC> Devices_Phone_QC { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasIndex(d => d.DeviceId).IsUnique();
                entity.Property(d => d.DeviceId).IsRequired().HasMaxLength(100);
                entity.Property(d => d.DeviceName).HasMaxLength(100);
                entity.Property(d => d.IpAddress).HasMaxLength(50);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(m => m.Content).IsRequired().HasMaxLength(1000);
                entity.Property(m => m.SenderDeviceId).IsRequired().HasMaxLength(100);
            });
        }
    }
}