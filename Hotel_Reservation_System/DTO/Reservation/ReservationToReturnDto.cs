namespace Hotel_Reservation_System.DTO.Reservation;

public class ReservationToReturnDto
{
    public int Id { get; set; }
    public DateTime Check_in_date { get; set; }
    public DateTime Check_out_date { get; set; }
    public double Total_Price { get; set; }
}
