using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.ViewModels.Reservation;

namespace Hotel_Reservation_System.Profiles.RepositoryProfile
{
	public class ReservationProfile : Profile
	{
		public ReservationProfile() 
		{
			CreateMap<ReservationDto, Reservation>();
			CreateMap<Reservation, ReservationToReturnDto>();
			CreateMap<CreateReservationViewModel, ReservationDto>();

		}
	}
}
