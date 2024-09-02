using Hotel_Reservation_System.DTO.Facility;
using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.Models;

namespace Hotel_Reservation_System.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IRepository<Room> _repositoryRoom;
        private readonly IRoomService _roomService;

        public ReservationService(IRepository<Reservation> repository,
                                  IRepository<Room> repositoryRooms,
                                  IRoomService roomService)
        {
            _repository = repository;
            _repositoryRoom = repositoryRooms;
            _roomService = roomService;
        }

        public ReservationToReturnDto GetById(int id)
        {
            var reservation = _repository.GetByID(id);
            var reservationToReturnDTO = reservation.MapOne<ReservationToReturnDto>();

            return reservationToReturnDTO;
        }

        public IEnumerable<ReservationToReturnDto> GetAllReservation()
        {
            var reservation = _repository.GetAll();
            var reservationsToReturnDTO = reservation.Select(f => f.MapOne<ReservationToReturnDto>());
            return reservationsToReturnDTO;
        }

        public ReservationToReturnDto Add(ReservationDto reservationDto, double totalPrice)
        {
            var isRoomAvailable = IsRoomAvailable(reservationDto.RoomId, reservationDto.Check_in_date, reservationDto.Check_out_date);

            if (isRoomAvailable)
            {
                var reservation = reservationDto.MapOne<Reservation>();

                var stayingDays = (reservationDto.Check_out_date - reservationDto.Check_in_date).Days;
                stayingDays = stayingDays == 0 ? 1 : stayingDays;
                reservation.Total_Price = totalPrice * stayingDays;
                reservation.UserId = 3;
                reservation = _repository.Add(reservation);
                _repository.SaveChanges();

                var reservationToReturnDTO = reservation.MapOne<ReservationToReturnDto>();

                return reservationToReturnDTO;
            }
            // what is the best way to return [null or throw exception] and Is there way to stop request and return a message 
            return null;

        }
        public ReservationToReturnDto Update(int id, ReservationDto reservationDto, double totalPrice)
        {
            var ReservationDt = _repository.GetByID(id);

            if (ReservationDt is not null)
            {
                var reservation = ReservationDt.MapOne<Reservation>();

                reservation.Id = id;
                reservation.Check_in_date = ReservationDt.Check_in_date;
                reservation.Check_out_date = ReservationDt.Check_out_date;

                var stayingDays = (reservationDto.Check_out_date - reservationDto.Check_in_date).Days;
                stayingDays = stayingDays == 0 ? 1 : stayingDays;
                reservation.Total_Price = totalPrice * stayingDays;
                reservation.UserId = 3;

                _repository.Update(reservation);
                _repository.SaveChanges();

                var reservationToReturnDTO = reservation.MapOne<ReservationToReturnDto>();

                return reservationToReturnDTO;
            }
            return null;
        }

        public bool CancelReservation(int reservationId)
        {
            var reservation = _repository.GetByID(reservationId);

            if (reservation == null)
                return false;

            if (reservation.ReservationStatus == ReservationStatus.CheckedOut || reservation.ReservationStatus == ReservationStatus.Cancelled)
                return false;

            reservation.ReservationStatus = ReservationStatus.Cancelled;
            _repository.Update(reservation);

            _repository.SaveChanges();

            return true;
        }

        public IEnumerable<RoomToReturnDto> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            var reservedRoomIds = GetReservedRoomIds(checkInDate, checkOutDate);

            var availableRooms = GetAvailableRooms(reservedRoomIds);

            return availableRooms;
        }
        public bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var overlappingReservations = _repository.GetAll()
                                                     .Any(r =>
                                                     r.RoomId == roomId &&
                                                     r.Check_in_date < checkOutDate &&
                                                     r.Check_out_date > checkInDate &&
                                                     r.ReservationStatus != ReservationStatus.Cancelled);

            return !overlappingReservations;
        }

        #region Private Method To Get Available Rooms

        private List<int> GetReservedRoomIds(DateTime checkInDate, DateTime checkOutDate)
        {
            var reservedRoomIds = _repository.GetAll()
                 .Where(r => r.Check_in_date < checkOutDate && r.Check_out_date > checkInDate)
                 .Select(r => r.Room.Id)
                 .Distinct()
                 .ToList();

            return reservedRoomIds;
        }

        private IEnumerable<RoomToReturnDto> GetAvailableRooms(List<int> reservedRoomIds)
        {
            var availableRooms = _roomService.GetAll()
                                             .Where(r => !reservedRoomIds.Contains(r.Id));
            return availableRooms;
        }

        #endregion

    }
}
