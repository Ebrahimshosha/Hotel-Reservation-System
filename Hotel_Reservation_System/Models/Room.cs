namespace Hotel_Reservation_System.Models;

public class Room: BaseModel
{
	public double Price { get; set; }
    public RoomType RoomType { get; set; }    
    public string Description { get; set; } = string.Empty;
    public ICollection<RoomImage> Images { get; set; } = new HashSet<RoomImage>();
    public ICollection<FacilityRoom> FacilityRooms { get; set; } = new HashSet<FacilityRoom>();
}
