namespace ParkingManagementSystem.Models
{
    public class ReservationParkingspaceViewModel
    {
        public IEnumerable<Reservation> Reservations { get; set; }
        public IEnumerable<ParkingSpace> ParkingSpaces { get; set; }
    }
}
