using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.Exceptions.Error;
using Hotel_Reservation_System.Mediators.ReservationMediator;
using Hotel_Reservation_System.ViewModels.Reservation;
using Hotel_Reservation_System.ViewModels.ResultViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
    public class ReservationsController : BaseApiController
    {
        private readonly IReservationMediator _mediator;

        public ReservationsController(IReservationMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public ResultViewModel<ReservationToReturnDto> ViewReservationDetails([FromRoute] int id)
        {
            var reservationDto = _mediator.GetById(id);
            return ResultViewModel<ReservationToReturnDto>.Sucess(reservationDto);
        }

        [HttpGet("")]
        public IActionResult GetAllReservation()
        {
            var facilitiesToReturnDto = _mediator.GetAllReservation();
            return Ok(facilitiesToReturnDto);
        }

        [HttpPost("")]
        public ResultViewModel<ReservationToReturnDto> MakeReservation([FromBody] CreateReservationViewModel viewModel)
        {
            var reservationDto = viewModel.MapOne<ReservationDto>();
            var reservationToReturnDto = _mediator.Add(reservationDto);
            if(reservationToReturnDto is null)
            {
                return ResultViewModel<ReservationToReturnDto>.Faliure(ErrorCode.BadRequest,"already reserved");
            }
            return ResultViewModel<ReservationToReturnDto>.Sucess(reservationToReturnDto);
        }

        [HttpPut("{id}")]
        public ResultViewModel<ReservationToReturnDto> UpdateReservationDto([FromRoute] int id, [FromBody] CreateReservationViewModel viewModel)
        {
            var reservationDto = viewModel.MapOne<ReservationDto>();
            var reservationToReturnDto = _mediator.Update(id, reservationDto);

            return ResultViewModel<ReservationToReturnDto>.Sucess(reservationToReturnDto);
        }

        [HttpDelete("{id}")]
        public ResultViewModel<bool> CancelReservation([FromRoute] int id)
        {
            var isDeleted = _mediator.CancelReservation(id);

            if (isDeleted)
            {
                return ResultViewModel<bool>.Sucess(true);
            }
            return ResultViewModel<bool>.Faliure(ErrorCode.ResourceNotFound, $" Reservation {id} NotFound");
        }
    }
}