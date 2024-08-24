using Hotel_Reservation_System.DTO.Reservation;

namespace Hotel_Reservation_System.Mediators.ReservationMediator
{
	public interface IReservationMediator
	{
  
		void Add(ReservationDto ReservationDto);

		IEnumerable<ReservationDto> getAllReservation();
		ReservationDto GetById(int id);
		ReservationDto Update(int id, ReservationDto reservationDto);
		bool DeleteReservation(int id);
	}
}
