using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingManagementSystem.Models
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public string? Location { get; set; }
        public string? AvailabilityStatus { get; set; }
        public int ParkingLotId { get; set; }

        public virtual ParkingLot? ParkingLot { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
        public virtual ICollection<CheckInOut>? CheckInOuts { get; set; }
    }
}
