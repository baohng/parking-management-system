using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Represent the databas tables
        public DbSet<UserInformation> UserInformations { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
         public DbSet<Reservation> Reservations { get; set; }
        public DbSet<CheckInOut> CheckInOuts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //If a record in the Reservation table is deleted, all corresponding records in the CheckInOut table wont be deleted.
            //but will have an exceptions
            modelBuilder.Entity<CheckInOut>()
                .HasOne(c => c.Reservations)
                .WithMany(r => r.CheckInOuts)
                .HasForeignKey(c => c.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            //If a record in the ParkingSpace table is deleted, all corresponding records in the CheckInOut table wont be deleted.
            //but will have an exceptions
            modelBuilder.Entity<CheckInOut>()
                .HasOne(c => c.ParkingSpace)
                .WithMany(p => p.CheckInOuts)
                .HasForeignKey(c => c.ParkingSpaceId)
                .OnDelete(DeleteBehavior.Restrict);
            //If a record in the ParkingSpace table is deleted, all corresponding records in the Reservation table wont be deleted.
            //but will have an exceptions
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ParkingSpace)
                .WithMany(p => p.Reservations)
                .HasForeignKey(r => r.ParkingSpaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}