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
		[HttpGet("Reservation")]
		public IActionResult GetAllFaility()
		{
			//User

			//Reservation
			var ReservationDto = _mediator.GetAllIReservation();
			return Ok(ReservationDto);
		}
	}
}
