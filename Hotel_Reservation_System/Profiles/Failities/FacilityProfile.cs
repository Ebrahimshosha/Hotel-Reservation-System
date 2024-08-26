
namespace Hotel_Reservation_System.Profiles.FailitiesProfiles;

public class FacilityProfile:Profile
{
    public FacilityProfile()
    {
        CreateMap<FacilityDto,Facility>();
        CreateMap<CreateFacilityViewModel, FacilityDto>();
        CreateMap<Facility, FacilityToReturnDto>();

    }
}
