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






	}
}
