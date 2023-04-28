using Microsoft.AspNetCore.Identity;

namespace ParkingManagementSystem.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; }
        public int ParkingSpaceId { get; set; }
        public DateTime TransactionsTime { get; set; } = DateTime.Now;
        public string PaymentInformation { get; set; }

        // Collection navigation property
        public virtual ParkingSpace ParkingSpace { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
