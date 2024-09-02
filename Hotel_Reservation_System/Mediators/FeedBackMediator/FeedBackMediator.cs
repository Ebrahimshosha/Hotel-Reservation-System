using Hotel_Reservation_System.DTO.FeedBack;
 
namespace Hotel_Reservation_System.Mediators.FeedBackMediator
{
	public class FeedBackMediator : IFeedBackMediator
	{
		private readonly StoreContext _context;

		public FeedBackMediator(StoreContext context)
		{
			_context = context;
		}

		public IEnumerable<FeedBack> GetAll()
		{
			return _context.FeedBacks.ToList();
		}

		public FeedBack Get(int id)
		{
			return _context.FeedBacks.Find(id);
		}

		public async Task<FeedBackDto> Add(AddFeedBackDto addFeedBackDto)
		{
			var feedback = new FeedBack
			{
				Text = addFeedBackDto.Text,
				UserId = addFeedBackDto.UserId,
				RoomId = addFeedBackDto.RoomId,
				Rating = addFeedBackDto.Rating,
				Submitted_at = addFeedBackDto.Submitted_at
			};

			_context.FeedBacks.Add(feedback);
			await _context.SaveChangesAsync();

			return new FeedBackDto
			{
				Text = feedback.Text,
				UserId = feedback.UserId,
				RoomId = feedback.RoomId,
				Rating = feedback.Rating,
				Submitted_at = feedback.Submitted_at
			};
		}


		public async Task<FeedBackDto> Update(int id, EditFeedBackDto editFeedBackDto)
		{
			var feedback = await _context.FeedBacks.FindAsync(id);
			if (feedback == null)
			{
				throw new KeyNotFoundException("FeedBack not found");
			}

			feedback.Text = editFeedBackDto.Text;
			feedback.Rating = editFeedBackDto.Rating;
			feedback.Submitted_at = editFeedBackDto.Submitted_at;

			_context.FeedBacks.Update(feedback);
			await _context.SaveChangesAsync();

			return new FeedBackDto
			{
				Text = feedback.Text,
				UserId = feedback.UserId,
				RoomId = feedback.RoomId,
				Rating = feedback.Rating,
				Submitted_at = feedback.Submitted_at
			};
		}


		public bool Delete(int id)
		{
			var feedback = _context.FeedBacks.Find(id);
			if (feedback == null)
			{
				return false;
			}

			_context.FeedBacks.Remove(feedback);
			_context.SaveChanges();
			return true;
		}

	}
}
