namespace ParkingManagementSystem.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int ParkingLotId { get; set; }
        public int SessionId { get; set; }
        public decimal PriceValue { get; set; }

        public virtual ParkingLot? ParkingLot { get; set; }
        public virtual Session? Session { get; set; }
    }
}
