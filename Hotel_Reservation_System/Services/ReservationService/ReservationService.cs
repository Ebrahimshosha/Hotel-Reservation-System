using Hotel_Reservation_System.DTO.Facility;
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

		public bool Delete(int id)
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
			var reservation = _repository.GetByID(id);
			var reservationDto = reservation.MapOne<ReservationDto>();
			return reservationDto;
		}

		public IEnumerable<ReservationDto> GetReservation()
		{
			var reservation = _repository.GetAll();
			var reservationDto = reservation.Select(f => f.MapOne<ReservationDto>());
			return reservationDto;
		}

		public ReservationDto Update(int id, ReservationDto reservationDto)
		{
			var ReservationDt = _repository.GetByID(id);

			if (ReservationDt is not null)
			{
				var reservation = ReservationDt.MapOne<Reservation>();

				reservation.Id = id;
				reservation.Check_in_date = ReservationDt.Check_in_date	;
				reservation.Check_out_date = ReservationDt.Check_out_date	;
				reservation.Total_Price = ReservationDt.Total_Price;

				_repository.Update(reservation);
				_repository.SaveChanges();

				var reservationsDto = reservation.MapOne<ReservationDto>();

				return reservationsDto;
			}
			return null;
		}

		void IReservationService.Add(ReservationDto reservationDto)
		{
			var reservation = reservationDto.MapOne<Reservation>();

			  _repository.Add(reservation);
			_repository.SaveChanges();
 		}

 
  
	}
}
