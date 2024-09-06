using Hotel_Reservation_System.DTO.Authorization;

namespace Hotel_Reservation_System.Profiles.Auth;

public class MappingProfileRole : Profile
{
    public MappingProfileRole()
    {
        CreateMap<ApplicationRole, RoleResponse>();
    }
}
