using Microsoft.EntityFrameworkCore;
using InternalBookingApp.Models;

namespace InternalBookingApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Resource)
                .WithMany()
                .HasForeignKey(b => b.ResourceId);

            // Seed data
            modelBuilder.Entity<Resource>().HasData(
                new Resource { Id = 1, Name = "Meeting Room Alpha", Description = "Large room with projector", Location = "3rd Floor", Capacity = 10, IsAvailable = true },
                new Resource { Id = 2, Name = "Company Car 1", Description = "Compact sedan", Location = "Parking Bay 5", Capacity = 4, IsAvailable = true }
            );
        }
    }
}