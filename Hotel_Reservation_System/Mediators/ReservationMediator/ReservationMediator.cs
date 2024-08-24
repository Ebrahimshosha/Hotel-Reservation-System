using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.Models;
using Hotel_Reservation_System.Services.ReservationService;

namespace Hotel_Reservation_System.Mediators.ReservationMediator
{
	public class ReservationMediator : IReservationMediator
	{

		private readonly IReservationService _reservationService;

		public ReservationMediator(IReservationService reservationService)
		{
			_reservationService = reservationService;
		}

		public void Add(ReservationDto reservationDto)
		{
			_reservationService.Add(reservationDto);
 
		}
	}
}
