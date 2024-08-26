namespace Hotel_Reservation_System.Services.RoomImages;

public interface IRoomImagesServices
{
    List<string> GetImageUrlsByRoomId(int roomId);
    Task<List<string>> AddImagesRoom(int roomId, List<IFormFile> images);
    Task<List<string>> UpdateImagesRoom(int roomId, List<IFormFile> images);
    bool DeleteRoomImages(int roomId, List<string> Images=null);
}
