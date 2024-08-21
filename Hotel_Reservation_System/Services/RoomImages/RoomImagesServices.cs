
using Hotel_Reservation_System.Data.migirations;
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
                uploadedImageUrls.Add(fileName);
                await AddRoomImageAsync(roomId, fileName);
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

    public bool Delete(int id)
    {
        var roomimages = _repository.Get(r => r.RoomId == id);

        if (roomimages is not null)
        {
            foreach (var item in roomimages)
            {
                _repository.Delete(item.Id);
                _repository.SaveChanges();
                return true;
            }
        }
        return false;
    }

    public bool DeleteRoomImagesByRoomId(int roomId)
    {
        var roomImages = _repository.Get(r => r.RoomId == roomId).ToList();

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
}
