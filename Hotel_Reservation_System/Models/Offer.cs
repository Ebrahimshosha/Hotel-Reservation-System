namespace Hotel_Reservation_System.Models;

public class Offer:BaseModel
{
    public DateOnly Start_date { get; set; }
    public DateOnly End_date { get; set; }
    public double Discount { get; set; }
    public ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
}
