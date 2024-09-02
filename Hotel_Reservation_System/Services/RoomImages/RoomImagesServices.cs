
using Hotel_Reservation_System.Models;

namespace Hotel_Reservation_System.Services.RoomImages;

public class RoomImagesServices : IRoomImagesServices
{
    private readonly IRepository<RoomImage> _repository;

    public RoomImagesServices(IRepository<RoomImage> repository)
    {
        _repository = repository;
    }

    public List<string> GetImageUrlsByRoomId(int roomId)
    {
        var roomImages = _repository.Get(rf => rf.RoomId == roomId).Select(rf => rf.Image_Url).ToList();
        return roomImages;
    }

    public async Task<List<string>> AddImagesRoom(int roomId, List<IFormFile> images)
    {
        var uploadedImageUrls = new List<string>();

        foreach (var image in images)
        {
            var fileName = await UploadImageAsync(image);
            if (!string.IsNullOrEmpty(fileName))
            {
                await AddRoomImageAsync(roomId, fileName);
                uploadedImageUrls.Add(fileName);
            }
        }
        return uploadedImageUrls;
    }

    public async Task<List<string>> UpdateImagesRoom(int roomId, List<IFormFile> images)
    {
        var existingRoomImages = _repository.Get(r => r.RoomId == roomId).ToList();

        foreach (var roomImage in existingRoomImages)
        {
            _repository.Delete(roomImage.Id);
        }

        _repository.SaveChanges();

        var updatedImageUrls = await AddImagesRoom(roomId, images);

        return updatedImageUrls;
    }

    public bool DeleteRoomImages(int RoomId, List<string> Images = null)
    {
        var ServerImages = Images?.Select(i => ExtractFileName(i)).ToList();

        bool hasRoomImages = Images != null && Images.Count > 0;

        var roomImages = _repository.Get(r => r.RoomId == RoomId && (!hasRoomImages || ServerImages!.Contains(r.Image_Url))).ToList();


        if (roomImages is null) return false;

        foreach (var roomImage in roomImages)
        {
            _repository.Delete(roomImage.Id);
        }

        _repository.SaveChanges();

        return true;
    }
    private async Task<string> UploadImageAsync(IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            return null;
        }
        return await DocumentSettings.UploadFileAsync(image, "Images");
    }
    private async Task AddRoomImageAsync(int roomId, string imageUrl)
    {
        _repository.Add(new RoomImage { RoomId = roomId, Image_Url = imageUrl });
        _repository.SaveChanges();
    }
    private string ExtractFileName(string fullUrl)
    {
        string baseUrl = "https://localhost:7223/Files/Images/";

        if (fullUrl.StartsWith(baseUrl))
        {
            return fullUrl.Substring(baseUrl.Length);
        }

        return null;
    }
}
