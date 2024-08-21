namespace Hotel_Reservation_System.DTO.Room;

public class CreateRoomDTO
{
    public double Price { get; set; }
    public string RoomType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<IFormFile> Images { get; set; } = null!;
    public List<int> FacilitiesIds { get; set; } = null!;
}
