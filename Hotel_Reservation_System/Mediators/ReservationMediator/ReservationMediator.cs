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


		public bool DeleteReservation(int id)
		{
			var reservation = _reservationService.GetById(id);
			if (reservation is not null)
			{
				_reservationService.DeleteReservation(id);
				return true;
			}
			return false;
		}

		public IEnumerable<ReservationDto> GetAllIReservation()
		{
			var ReservationDto = _reservationService.GetReservations();

			

			return ReservationDto;
		}

		public ReservationDto GetById(int id)
		{

			var ReservationDto = _reservationService.GetById(id);
			return ReservationDto;
		}
	}
}
