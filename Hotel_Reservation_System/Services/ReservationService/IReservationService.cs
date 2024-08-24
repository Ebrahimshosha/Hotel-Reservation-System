using Hotel_Reservation_System.DTO.Reservation;

namespace Hotel_Reservation_System.Services.ReservationService
{
	public interface IReservationService
	{
		ReservationDto GetById(int id);
		IEnumerable<ReservationDto> GetReservation();
		//ReservationDto Add(ReservationDto reservationDto);
		ReservationDto Update(int id, ReservationDto reservationDto);
		bool Delete(int id);

		void Add(ReservationDto reservationDto );

		List<ReservationDto> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate);


	}
}
