using Hotel_Reservation_System.DTO.Reservation;

namespace Hotel_Reservation_System.Services.ReservationService
{
    public interface IReservationService
    {
        IEnumerable<ReservationToReturnDto> GetAllReservation();
        ReservationToReturnDto GetById(int id);
        ReservationToReturnDto Add(ReservationDto reservationDto, double totalPrice);
        ReservationToReturnDto Update(int id, ReservationDto reservationDto, double totalPrice);
        bool CancelReservation(int reservationId);
        IEnumerable<RoomToReturnDto> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate);
        bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate);
    }
}
