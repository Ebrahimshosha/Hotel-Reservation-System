using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.Models;
using Hotel_Reservation_System.Services.ReservationService;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Mediators.ReservationMediator
{
    public class ReservationMediator : IReservationMediator
    {
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;

        public ReservationMediator(IReservationService reservationService, IRoomService roomService)
        {
            _reservationService = reservationService;
            _roomService = roomService;
        }

        public ReservationToReturnDto GetById(int id)
        {
            var reservationToReturnDto = _reservationService.GetById(id);

            return reservationToReturnDto;
        }

        public IEnumerable<ReservationToReturnDto> GetAllReservation()
        {
            var reservationsToReturnDto = _reservationService.GetAllReservation();

            return reservationsToReturnDto;
        }
        public ReservationToReturnDto Add(ReservationDto reservationDto)
        {
            var totalPrice = _roomService.CalculateTotalPrice(reservationDto.RoomId);
            var reservationToReturnDto = _reservationService.Add(reservationDto, totalPrice);

            return reservationToReturnDto;
        }

        public ReservationToReturnDto Update(int id, ReservationDto reservationDto)
        {
            var totalPrice = _roomService.CalculateTotalPrice(reservationDto.RoomId);
            var reservationToReturnDto = _reservationService.Update(id, reservationDto, totalPrice);

            return reservationToReturnDto;
        }

        public bool CancelReservation(int id)
        {
            var facilty = _reservationService.GetById(id);

            if (facilty is not null)
            {
                var isCanceled = _reservationService.CancelReservation(id);
                if (isCanceled)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

