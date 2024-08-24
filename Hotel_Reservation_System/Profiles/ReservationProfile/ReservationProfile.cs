using Hotel_Reservation_System.DTO.Reservation;

namespace Hotel_Reservation_System.Profiles.RepositoryProfile
{
	public class ReservationProfile : Profile
	{
		public ReservationProfile() 
		{
			CreateMap<ReservationDto, Reservation>();


		}
	}
}
