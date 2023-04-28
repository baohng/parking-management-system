namespace ParkingManagementSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }

        // Navigation property
        public virtual Transaction Transaction { get; set; }
    }
}
