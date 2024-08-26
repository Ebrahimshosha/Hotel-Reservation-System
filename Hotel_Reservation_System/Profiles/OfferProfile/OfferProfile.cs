using Hotel_Reservation_System.DTO.Offer;
using Hotel_Reservation_System.ViewModels.Offer;

namespace Hotel_Reservation_System.Profiles.OfferProfile
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<AddOfferDto, Offer>()
                .ForMember(d => d.Start_date, o => o.MapFrom(src => src.Start_date))
                .ForMember(d => d.End_date, o => o.MapFrom(src => src.End_date))
                .ForMember(d=> d.Discount, o => o.MapFrom(src => src.Discount));

            CreateMap<EditOfferDto, Offer>()
                .ForMember(d => d.Start_date, o => o.MapFrom(src => src.Start_date))
                .ForMember(d => d.End_date, o => o.MapFrom(src => src.End_date))
                .ForMember(d => d.Discount, o => o.MapFrom(src => src.Discount));

            CreateMap<Offer, OfferDTO>()
                .ForMember(d => d.Start_date, o => o.MapFrom(src => src.Start_date))
                .ForMember(d => d.End_date, o => o.MapFrom(src => src.End_date))
                .ForMember(d => d.Discount, o => o.MapFrom(src => src.Discount))
                .ForMember(d => d.RoomIds, o => o.MapFrom(src => src.OfferRooms.Select(or => or.RoomId).ToList()));

            CreateMap<OfferDTO, OfferViewModel>()
                .ForMember(d => d.Start_date, o => o.MapFrom(src => src.Start_date))
                .ForMember(d => d.End_date, o => o.MapFrom(src => src.End_date))
                .ForMember(d => d.Discount, o => o.MapFrom(src => src.Discount))
                .ForMember(d => d.RoomIds, o => o.MapFrom(src => src.RoomIds));

            CreateMap<CreateOfferViewModel, AddOfferDto>();
            CreateMap<CreateOfferViewModel, EditOfferDto>();
        }
    }

    }

