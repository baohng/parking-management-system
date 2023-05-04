using Microsoft.AspNetCore.Identity;

namespace ParkingManagementSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UserId { get; set; }   // use string type to match the Id type of AspNetUsers table
        public int ParkingSpaceId { get; set; }
        public DateTime ReservationStartTime { get; set; }
        public DateTime ReservationEndTime { get; set; }

        public virtual ParkingSpace? ParkingSpace { get; set; }   // navigation property to ParkingSpace
        public virtual IdentityUser? User { get; set; }           // navigation property to AspNetUsers

        public virtual ICollection<CheckInOut>? CheckInOuts { get; set; }
    }
}