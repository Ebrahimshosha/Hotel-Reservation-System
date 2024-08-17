using AutoMapper;
using Hotel_Reservation_System.DTO.Room;
using Hotel_Reservation_System.Helpers.PictureUrlResolve;
using Hotel_Reservation_System.Mediators.RoomMediator;
using Hotel_Reservation_System.ViewModels.Room;

namespace Hotel_Reservation_System.Profiles.Roomprofiles;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<CreateRoomViewModel, CreateRoomDTO>();

        CreateMap<CreateRoomDTO, RoomDTO>(); // Is there more efficient way to mapping from CreateRoomDTO to RoomDTO in RoomMediator?

        CreateMap<Room, RoomDTO>()
            .ForMember(d => d.RoomType, o => o.MapFrom(s => s.RoomType.ToString()));

        CreateMap<RoomDTO, RoomViewModel>()
            .ForMember(d => d.Image_Url, o => o.MapFrom<RoomPictureUrlResolve>());

        CreateMap<RoomDTO, Room>()
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => Enum.Parse<RoomType>(src.RoomType, true)));
        
        CreateMap<AvailabileRoomViewModel, Room>();
    }
}
