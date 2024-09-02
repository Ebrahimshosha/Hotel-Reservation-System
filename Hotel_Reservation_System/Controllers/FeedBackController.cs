using Hotel_Reservation_System.DTO.FeedBack;
using Hotel_Reservation_System.Mediators.FeedBackMediator;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedBacksController : ControllerBase
	{
		private readonly IFeedBackMediator _feedBackMediator;

		public FeedBacksController(IFeedBackMediator feedBackMediator)
		{
			_feedBackMediator = feedBackMediator;
		}

		// GET: api/FeedBacks
		[HttpGet]
		public ActionResult<IEnumerable<FeedBack>> GetFeedBacks()
		{
			return Ok(_feedBackMediator.GetAll());
		}

		// GET: api/FeedBacks/5
		[HttpGet("{id}")]
		public ActionResult<FeedBack> GetFeedBack(int id)
		{
			var feedback = _feedBackMediator.Get(id);

			if (feedback == null)
			{
				return NotFound();
			}

			return Ok(feedback);
		}

		// POST: api/FeedBacks
		[HttpPost]
		public async Task<ActionResult<FeedBackDto>> PostFeedBack(AddFeedBackDto addFeedBackDto)
		{
			var feedback = await _feedBackMediator.Add(addFeedBackDto);
			return CreatedAtAction(nameof(GetFeedBack), new { id = feedback.Id}, feedback);
		}

		// PUT: api/FeedBacks/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutFeedBack(int id, EditFeedBackDto editFeedBackDto)
		{
			try
			{
				var updatedFeedback = await _feedBackMediator.Update(id, editFeedBackDto);
				return Ok(updatedFeedback);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
		}

		// DELETE: api/FeedBacks/5
		[HttpDelete("{id}")]
		public IActionResult DeleteFeedBack(int id)
		{
			var deleted = _feedBackMediator.Delete(id);
			if (!deleted)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
