

namespace Hotel_Reservation_System
{
    public class paymentViewModel
    {
        public Reservation? Reservation { get; set; } = null!;

        public int ReservationID { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
    }
}
