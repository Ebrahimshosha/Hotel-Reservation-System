namespace Hotel_Reservation_System.DTO.FeedBack
{
	public class EditFeedBackDto
	{
		public string Text { get; set; } = string.Empty;
  		public int Rating { get; set; }
		public DateTime Submitted_at { get; set; }

	}
}
