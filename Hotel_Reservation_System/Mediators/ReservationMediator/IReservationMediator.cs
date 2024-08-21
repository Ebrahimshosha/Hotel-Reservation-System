using Hotel_Reservation_System.DTO.Reservation;

namespace Hotel_Reservation_System.Mediators.ReservationMediator
{
	public interface IReservationMediator
	{
		IEnumerable<ReservationDto> GetAllIReservation(); 
		ReservationDto GetById(int id);

		//ReservationDto Add(CreateFacilityViewMo viewModel);
		//ReservationDto Update(int id, CreateFacilityViewMo viewModel);
		bool DeleteReservation(int id);
	}
}
