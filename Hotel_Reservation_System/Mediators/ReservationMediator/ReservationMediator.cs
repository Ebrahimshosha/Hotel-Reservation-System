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



		public ReservationDto Update(int id, ReservationDto reservationDto)
		{
			var reservationrDto = reservationDto.MapOne<ReservationDto>();

			var reservationsDto = _reservationService.Update(id, reservationrDto);

			return reservationsDto;
		}

		ReservationDto IReservationMediator.GetById(int id)
		{
			var reservationsDto = _reservationService.GetById(id);
			return reservationsDto;
		}
		 
		public IEnumerable<ReservationDto> getAllReservation()
		{
			var reservationsDto = _reservationService.GetReservation();
			return reservationsDto;
		}


		 

		 

		public bool DeleteReservation(int id)
		{
			var facilty = _reservationService.GetById(id);
			if (facilty is not null)
			{
				_reservationService.Delete(id);
				return true;
			}
			return false;
		}
	}

}

