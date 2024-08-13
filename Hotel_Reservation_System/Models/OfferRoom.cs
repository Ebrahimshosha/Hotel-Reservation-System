namespace Hotel_Reservation_System.Models;

public class OfferRoom : BaseModel
{
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
    public int OfferId { get; set; }
    public Offer offer { get; set; } = null!;
}
