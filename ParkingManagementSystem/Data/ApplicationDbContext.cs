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
        DbSet<UserInformation> UserInformations { get; set; }
        DbSet<Session> Sessions { get; set; }
        DbSet<ParkingLot> ParkingLots { get; set; }
        DbSet<Price> Prices { get; set; }
        DbSet<ParkingSpace> ParkingSpaces { get; set; }
        DbSet<Reservation> Reservations { get; set; }
    }
}