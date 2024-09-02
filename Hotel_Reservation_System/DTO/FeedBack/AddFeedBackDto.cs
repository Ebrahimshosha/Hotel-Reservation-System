namespace Hotel_Reservation_System.DTO.FeedBack
{
	public class AddFeedBackDto
	{
		public string Text { get; set; } = string.Empty;
		public int UserId { get; set; }
 		public int RoomId { get; set; }
		public int Rating { get; set; }
		public DateTime Submitted_at { get; set; }
	}
}
