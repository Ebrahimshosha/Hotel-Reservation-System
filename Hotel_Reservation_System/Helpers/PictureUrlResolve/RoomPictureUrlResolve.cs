
namespace Hotel_Reservation_System.Helpers.PictureUrlResolve;

public class RoomPictureUrlResolve : IValueResolver<RoomDTO, RoomViewModel, string>
{

    private readonly IConfiguration _configuration;

    public RoomPictureUrlResolve(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(RoomDTO source, RoomViewModel destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.Image_Url))
        {
            return $"{_configuration["ApiBaseUrl"]}Files/Images/{source.Image_Url}";
        }
        return string.Empty;
    }
}

