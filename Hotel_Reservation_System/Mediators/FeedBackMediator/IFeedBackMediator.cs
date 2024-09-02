using Hotel_Reservation_System.DTO.FeedBack;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Hotel_Reservation_System.Mediators.FeedBackMediator
{
	public interface IFeedBackMediator
	{
		IEnumerable<FeedBack> GetAll();
		FeedBack Get(int id);
		Task<FeedBackDto> Add(AddFeedBackDto addFeedBackDto);
		Task<FeedBackDto> Update(int id, EditFeedBackDto editFeedBackDto);
		bool Delete(int id);
	}
}
