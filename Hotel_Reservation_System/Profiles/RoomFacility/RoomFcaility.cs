using Hotel_Reservation_System.DTO.RoomFacility;

namespace Hotel_Reservation_System.Profiles.RoomFacilityProfiles;

public class RoomFcaility : Profile
{
    public RoomFcaility()
    {
        CreateMap<RoomFacilityDto,FacilityRoom>();
    }
}
