using Hotel_Reservation_System.DTO.Facility;
using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.Models;

namespace Hotel_Reservation_System.Services.ReservationService
{
	public class ReservationService : IReservationService
	{
		private readonly IRepository<Reservation> _repository;
		private readonly IRepository<Room> _repositoryRoom;


		public ReservationService(IRepository<Reservation> repository, IRepository<Room> repositoryRooms)
		{
			_repository = repository;
			_repositoryRoom = repositoryRooms;


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
			var reservationsDto = reservation.Select(f => f.MapOne<ReservationDto>());
			return reservationsDto;
		}

		public ReservationDto Update(int id, ReservationDto reservationDto)
		{
			var ReservationDt = _repository.GetByID(id);

			if (ReservationDt is not null)
			{
				var reservation = ReservationDt.MapOne<Reservation>();

				reservation.Id = id;
				reservation.Check_in_date = ReservationDt.Check_in_date;
				reservation.Check_out_date = ReservationDt.Check_out_date;
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



		public IEnumerable<RoomToReturnDto> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
		{

			var reservedRoomIds = GetReservedRoomIds(checkInDate, checkOutDate);

			var availableRooms = GetAvailableRooms(reservedRoomIds);

			var roomsToReturnDto = availableRooms.Map<RoomToReturnDto>();
			return roomsToReturnDto; ;
		}

		private List<int> GetReservedRoomIds(DateTime checkInDate, DateTime checkOutDate)
		{
			var reservedRoomIds = _repository.GetAll()
				 .Where(r => r.Check_in_date < checkOutDate && r.Check_out_date > checkInDate)
				 .Select(r => r.Room.Id)
				 .Distinct()
				 .ToList();

			return reservedRoomIds;
		}

		private IQueryable<Room> GetAvailableRooms(List<int> reservedRoomIds)
		{
			var availableRooms = _repositoryRoom.GetAll()
											.Where(r => !reservedRoomIds.Contains(r.Id));

			return availableRooms;
		}

		List<ReservationDto> IReservationService.GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
		{
			throw new NotImplementedException();
		}



		//public List<ReservationDto> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
		//{
		//	var availableRooms = _repositoryRoom.GetAll()
		//		.Where(room => !_repository.GetAll()
		//			.Any(r => r.Room.Id == room.Id && r.Check_in_date < checkOutDate && r.Check_out_date > checkInDate))
		//		.Select(room => new ReservationDto
		//		{
		//			Check_in_date = DateTime.MinValue,

		//			Check_out_date = DateTime.MinValue,
		//			Total_Price = 0,
		//			RoomDTO = new List<RoomDTO> { room.MapOne<RoomDTO>() }
		//		})
		//		.ToList();

		//	if (availableRooms.Count > 0)
		//	{
		//		return availableRooms;
		//	}
		//	else
		//	{
		//		return new List<ReservationDto> { new ReservationDto { RoomDTO = new List<RoomDTO> { new RoomDTO { RoomType = "لا توجد غرف متاحة في هذه الفترة" } } } };
		//	}
		//}



		///api/Reservation/SearchRoom?checkInDate=2024-08-01T14:00:00&checkOutDate=2024-08-05T11:00:00
		//public List<int> GetReservedRoomIds(DateTime checkInDate, DateTime checkOutDate)
		//{
		//	var reservedRoomIds = _repository.GetAll()
		//		.Where(r => r.Check_in_date < checkOutDate && r.Check_out_date > checkInDate)
		//		.Select(r => r.Room.Id)
		//		.Distinct()
		//		.ToList();

		//	return reservedRoomIds;
		//}

	}
	}
