using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.Exceptions.Error;
using Hotel_Reservation_System.Mediators.ReservationMediator;
using Hotel_Reservation_System.ViewModels.ResultViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{

 	public class ReservationController : BaseApiController
	{
		private readonly IReservationMediator _mediator;

		public ReservationController(IReservationMediator mediator)
		{
			_mediator = mediator;
		}
		 
		//2024-08-23T14:30:00Z

		[HttpPost("AddReservation")]
 		public ResultViewModel<bool> AddReservation([FromBody] ReservationDto reservationDto)
		{
			_mediator.Add(reservationDto);
			return ResultViewModel<bool>.Sucess(true);
		}
		[HttpGet("GetAllReservation")]
		public IActionResult GetAllReservation()
		{
			var facilitiesToReturnDto = _mediator.getAllReservation();
			return Ok(facilitiesToReturnDto);
		}
		[HttpGet("{id}")]
		public ResultViewModel<ReservationDto> GetReservationsById([FromRoute] int id)
		{
			var reservationDto = _mediator.GetById(id);
			return ResultViewModel<ReservationDto>.Sucess(reservationDto);
		}


		[HttpPut("{id}")]
		public ResultViewModel<ReservationDto> UpdateReservationDto([FromRoute] int id, [FromBody] ReservationDto reservationDto)
		{
			var reservationsDto = _mediator.Update(id, reservationDto);
			return ResultViewModel<ReservationDto>.Sucess(reservationDto);
		}

		[HttpDelete("{id}")]
		public ResultViewModel<bool> DeleteReservation([FromRoute] int id)
		{
			var isDeleted = _mediator.DeleteReservation(id);

			if (isDeleted)
			{

				return ResultViewModel<bool>.Sucess(true);
			}
			return ResultViewModel<bool>.Faliure(ErrorCode.ResourceNotFound,$" Reservation {id} NotFound") ;
		}


		[HttpGet("SearchRoom")]
		public ResultViewModel<List<ReservationDto>> SearchRoom(DateTime checkInDate, DateTime checkOutDate)
		{
			var availableRooms = _mediator.GetAvailableRooms(checkInDate, checkOutDate);
			return ResultViewModel<List<ReservationDto>>.Sucess(availableRooms);
		}


	}
}
