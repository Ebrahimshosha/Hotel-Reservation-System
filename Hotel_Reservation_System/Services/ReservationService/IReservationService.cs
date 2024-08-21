using Hotel_Reservation_System.DTO.Reservation;

namespace Hotel_Reservation_System.Services.ReservationService
{
	public interface IReservationService
	{

		ReservationDto GetById(int id);
		IEnumerable<ReservationDto> GetReservations();

		//ReservationDto Add(FacilityDto facilityDto);
		//ReservationDto Update(int id, FacilityDto facilityDto);

		bool DeleteReservation(int id);
	}
}
