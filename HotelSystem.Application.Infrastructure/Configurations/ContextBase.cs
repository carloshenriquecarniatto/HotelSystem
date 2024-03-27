using HotelSystem.Application.Entities.Customer;
using HotelSystem.Application.Entities.Hotels;
using HotelSystem.Application.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Application.Infrastructure.Configurations;

public class ContextBase(DbContextOptions<ContextBase> options) : DbContext(options)
{
    public DbSet<HotelBooking> HotelBookings { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<HotelBooking>()
            .HasKey(c => c.Id);
        builder.Entity<HotelBooking>()
            .Navigation(c => c.Hotel)
            .AutoInclude();
        builder.Entity<HotelBooking>()
            .Navigation(c => c.Client)
            .AutoInclude();
        builder.Entity<Client>()
            .HasKey(c => c.Id);
        builder.Entity<Client>(builder =>
        {
            builder
                .HasMany(bookings => bookings.HotelBookings)
                .WithOne(booking => booking.Client)
                .HasForeignKey(booking => booking.ClientId)
                .IsRequired();
        });
        builder.Entity<Hotel>(builder =>
        {
            builder
                .HasMany(bookings => bookings.HotelBookings)
                .WithOne(booking => booking.Hotel)
                .HasForeignKey(booking => booking.HotelId)
                .IsRequired();
        });
        builder.Entity<Hotel>()
            .HasKey(c => c.Id);
        base.OnModelCreating(builder);
    }

}
