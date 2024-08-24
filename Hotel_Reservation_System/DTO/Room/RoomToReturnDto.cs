namespace Hotel_Reservation_System.DTO.Room;

public class RoomToReturnDto
{
    public int Id { get; set; }
    public double Price { get; set; }
    public RoomType RoomType { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string Description { get; set; } = string.Empty;
    public string Image_Url { get; set; } = string.Empty;
}
