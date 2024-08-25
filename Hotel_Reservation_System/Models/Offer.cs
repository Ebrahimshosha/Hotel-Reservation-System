namespace Hotel_Reservation_System.Models;

public class Offer:BaseModel
{
    public DateTime Start_date { get; set; }
    public DateTime End_date { get; set; }
    public double Discount { get; set; }
    public ICollection<OfferRoom> OfferRooms { get; set; } = new HashSet<OfferRoom>();
}
