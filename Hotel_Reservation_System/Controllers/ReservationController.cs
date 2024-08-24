using Hotel_Reservation_System.DTO.Reservation;
using Hotel_Reservation_System.Mediators.ReservationMediator;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{

 	public class ReservationController : Controller
	{
		private readonly IReservationMediator _mediator;

		public ReservationController(IReservationMediator mediator)
		{
			_mediator = mediator;
		}
		 
		//2024-08-23T14:30:00Z

		[HttpPost("AddReservation")]
 		public IActionResult AddReservation([FromBody] ReservationDto reservationDto)
		{
			_mediator.Add(reservationDto);
			return Ok();
		}
		[HttpGet("GetAllReservation")]
		public IActionResult GetAllReservation()
		{
			var facilitiesToReturnDto = _mediator.getAllReservation();
			return Ok(facilitiesToReturnDto);
		}
		[HttpGet("{id}")]
		public ReservationDto GetReservationsById([FromRoute] int id)
		{
			var reservationDto = _mediator.GetById(id);
			return reservationDto;
		}


		[HttpPut("{id}")]
		public IActionResult UpdateReservationDto([FromRoute] int id, [FromBody] ReservationDto reservationDto)
		{
			var reservationsDto = _mediator.Update(id, reservationDto);
			return Ok(reservationsDto);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteReservation([FromRoute] int id)
		{
			var isDeleted = _mediator.DeleteReservation(id);

			if (isDeleted)
			{

				return Ok(true);
			}
			return Ok(false);
		}





	}
}
