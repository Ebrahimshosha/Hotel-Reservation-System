
namespace Hotel_Reservation_System.Helpers.PictureUrlResolve;

public class RoomPictureUrlResolve : IValueResolver<Room, RoomToReturnDto, string>
{

    private readonly IConfiguration _configuration;

    public RoomPictureUrlResolve(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Resolve(Room source, RoomToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.Image_Url))
        {
            return $"{_configuration["ApiBaseUrl"]}Files/Images/{source.Image_Url}";
        }
        return string.Empty;
    }
}

