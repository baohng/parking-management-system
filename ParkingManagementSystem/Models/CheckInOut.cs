//using Microsoft.AspNetCore.Identity;
//using System.ComponentModel.DataAnnotations;

//namespace ParkingManagementSystem.Models
//{
//    public class CheckInOut
//    {
//        [Key]
//        public int Id { get; set; }

//        // Foreign keys
//        public int ReservationId { get; set; }
//        public int ParkingSpaceId { get; set; }

//        // Navigation properties
//        public virtual Reservation Reservations { get; set; }
//        public virtual ParkingSpace ParkingSpace { get; set; }


//        public DateTime CheckInTime { get; set; }
//        public DateTime CheckOutTime { get; set; }
//        public DateTime Duration { get; set; }
//        public decimal AmountPaid { get; set; }
//        public string PaymentStatus { get; set; }
//    }
//}
