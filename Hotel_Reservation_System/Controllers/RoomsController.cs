
namespace Hotel_Reservation_System.Controllers;

public class RoomsController : BaseApiController
{
    private readonly IRoomMediator _mediator;

    public RoomsController(IRoomMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public IActionResult Room([FromRoute] int id)
    {
        var roomToreturnDto = _mediator.GetById(id);
        var roomViewModel = roomToreturnDto.MapOne<RoomViewModel>();

        return Ok(roomViewModel);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddRoom([FromForm] CreateRoomViewModel viewModel)
    {
        var createRoomDTO = viewModel.MapOne<CreateRoomDTO>();
        var roomToreturnDTO = await _mediator.Add(createRoomDTO);
        var roomViewModel = roomToreturnDTO.MapOne<RoomViewModel>();
        return Ok(roomViewModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoom([FromRoute] int id, [FromForm] CreateRoomViewModel viewModel)
    {
        var createRoomDTO = viewModel.MapOne<CreateRoomDTO>();
        var roomToreturnDTO = await _mediator.Update(id, createRoomDTO);
        var roomViewModel = roomToreturnDTO.MapOne<RoomViewModel>();
        return Ok(roomViewModel);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoom([FromRoute] int id)
    {
        var isDeleted = _mediator.Delete(id);
        if (isDeleted)
        {
            return Ok();
        }
        return NotFound();
    }



    [HttpGet("ViewRoomAvailability")]
    public IActionResult ViewRoomAvailability([FromBody] AvailabileRoomViewModel ViewModel)
    {
        var roomsDTO = _mediator.ViewRoomAvailability(ViewModel);
        var roomsViewModel = roomsDTO.Select(r => r.MapOne<RoomViewModel>());
        return Ok(roomsViewModel);

    }
}
