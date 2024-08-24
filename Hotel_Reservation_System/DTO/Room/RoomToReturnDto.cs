namespace Hotel_Reservation_System.DTO.Room;

public class RoomToReturnDto
{
    public int Id { get; set; }
    public double Price { get; set; }
    public RoomType RoomType { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Images { get; set; } = null!;
    public List<int> FacilitiesIds { get; set; } = null!;

}
