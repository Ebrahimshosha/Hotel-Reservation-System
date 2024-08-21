using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.Models;

namespace Hotel_Reservation_System.Services.ReservationService
{
	public class ReservationService : IReservationService
	{
		private readonly IRepository<Reservation> _repository;

		public ReservationService(IRepository<Reservation> repository)
		{
			_repository = repository;
		}
		public bool DeleteReservation(int id)
		{
			var room = _repository.GetByID(id);

			if (room is not null)
			{
				_repository.Delete(id);
				_repository.SaveChanges();
				return true;
			}
			return false;
		}

		public ReservationDto GetById(int id)
		{
			var Reservation = _repository.GetByID(id);
			var ReservationDto = Reservation.MapOne<ReservationDto>();
			return ReservationDto;
		}


		public IEnumerable<ReservationDto> GetReservations()
		{
			var Reservations = _repository.GetAll();

			var ReservationDto = Reservations.Select(r => new ReservationDto
			{
				Check_in_date = r.Check_in_date,
				Check_out_date = r.Check_out_date,
				Total_Price = r.Total_Price,
				RoomList = r.RoomList.Select(room => new RoomDTO
				{
				}).ToList()
			});

			return ReservationDto;
		}


	}
}
