using Hotel_Reservation_System.DTO.RoomFacility;

namespace Hotel_Reservation_System.Profiles.RoomFacilityProfiles;

public class RoomFcailityDto : Profile
{
    public RoomFcailityDto()
    {
        CreateMap<RoomFacilityDto,RoomFacility>();
    }
}
