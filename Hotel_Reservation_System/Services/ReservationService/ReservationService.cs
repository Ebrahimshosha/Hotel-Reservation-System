using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.Models;

namespace Hotel_Reservation_System.Services.ReservationService
{
	public class ReservationService : IReservationService
	{
		private readonly IRepository<Reservation> _repository;


		public ReservationService(IRepository<Reservation> repository, IRepository<Room> roomRepository)
		{
			_repository = repository;
 		}

		 


		void IReservationService.Add(ReservationDto reservationDto)
		{
			var reservation = reservationDto.MapOne<Reservation>();

			  _repository.Add(reservation);
			_repository.SaveChanges();
 		}

 
  
	}
}
