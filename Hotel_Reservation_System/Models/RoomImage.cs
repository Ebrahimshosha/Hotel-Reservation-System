namespace Hotel_Reservation_System.Models;

public class RoomImage : BaseModel
{
    public string Image_Url { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
}
