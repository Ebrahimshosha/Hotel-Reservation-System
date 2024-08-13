namespace Hotel_Reservation_System.Models;

public class Room: BaseModel
{
    public double Price { get; set; }
    public RoomType RoomType { get; set; }    
    public bool IsAvailable { get; set; }
    public string Description { get; set; } = string.Empty;
    public Facilities Facilities { get; set; }
    public string Image_Url { get; set; } = string.Empty ;
}
