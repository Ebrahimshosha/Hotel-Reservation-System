using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel_Reservation_System.DTO.Reservation
{
	public class ReservationDto
	{

		public DateTime Check_in_date { get; set; }
		public DateTime Check_out_date { get; set; }
		public double Total_Price { get; set; }

		public IEnumerable<RoomDTO> RoomList { get; set; }


	}
}
