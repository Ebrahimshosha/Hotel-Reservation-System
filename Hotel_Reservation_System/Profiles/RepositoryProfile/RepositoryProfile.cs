using Hotel_Reservation_System.DTO.Reservation;

namespace Hotel_Reservation_System.Profiles.RepositoryProfile
{
	public class RepositoryProfile : Profile
	{
		public RepositoryProfile() 
		{
			CreateMap<ReservationDto, Reservation>();


		}
	}
}
