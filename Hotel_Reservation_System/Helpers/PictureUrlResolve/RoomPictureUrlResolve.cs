
using Microsoft.IdentityModel.Tokens;

namespace Hotel_Reservation_System.Helpers.PictureUrlResolve;

public class RoomPictureUrlResolve : IValueResolver<RoomToReturnDto, RoomViewModel, List<string>>
{

    private readonly IConfiguration _configuration;

    public RoomPictureUrlResolve(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<string> Resolve(RoomToReturnDto source, RoomViewModel destination, List<string> destMember, ResolutionContext context)
    {
        var images = new List<string>();
        foreach (var img in source.Images)
        {
            if (!string.IsNullOrEmpty(img))
            {
                images.Add($"{_configuration["ApiBaseUrl"]}Files/Images/{img}");
            }
        }
        return images;
    }
}

