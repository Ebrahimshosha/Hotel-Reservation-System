﻿using AutoMapper;
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

        CreateMap<CreateRoomDTO, RoomDTO>();

        CreateMap<RoomDTO, Room>()
           .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => Enum.Parse<RoomType>(src.RoomType, true)));

        CreateMap<Room, RoomToReturnDto>();

        CreateMap<RoomToReturnDto, RoomViewModel>()
            .ForMember(d => d.images, o => o.MapFrom<RoomPictureUrlResolve>())
            .ForMember(d => d.RoomType, o => o.MapFrom(s => s.RoomType.ToString()));

    }
}
