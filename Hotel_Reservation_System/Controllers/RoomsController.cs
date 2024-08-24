
namespace Hotel_Reservation_System.Controllers;

public class RoomsController : BaseApiController
{
    private readonly IRoomMediator _mediator;

    public RoomsController(IRoomMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getAllRooms")]
    public  IActionResult getAllRooms()
    {
        var roomDTO =  _mediator.GetAll();
        var roomViewModel = roomDTO.MapOne<RoomViewModel>();
        return Ok(roomViewModel);
    }  
    [HttpPost("Room")]
    public async Task<IActionResult> AddRoom([FromForm] CreateRoomViewModel viewModel)
    {
        var createRoomDTO = viewModel.MapOne<CreateRoomDTO>();
        var roomDTO = await _mediator.Add(createRoomDTO);
        var roomViewModel = roomDTO.MapOne<RoomViewModel>();
        return Ok(roomViewModel);
    }

    [HttpPut("Room{id}")]
    public async Task<IActionResult> UpdateRoom([FromRoute] int id, [FromForm] CreateRoomViewModel viewModel)
    {
        var createRoomDTO = viewModel.MapOne<CreateRoomDTO>();
        var roomDTO = await _mediator.Update(id, createRoomDTO);
        var roomViewModel = roomDTO.MapOne<RoomViewModel>();
        return Ok(roomViewModel);
    }

    [HttpDelete("Room")]
    public IActionResult DeleteRoom([FromRoute] int id)
    {
        _mediator.Delete(id);
        return Ok();
    }

    [HttpPost("ViewRoomAvailability")]
    public IActionResult ViewRoomAvailability([FromBody] AvailabileRoomViewModel ViewModel)
    {
        var roomsDTO = _mediator.ViewRoomAvailability(ViewModel);
        var roomsViewModel = roomsDTO.Select(r=>r.MapOne<RoomViewModel>());
        return Ok(roomsViewModel);

    }

    [HttpGet("{id}")]
    public IActionResult Room([FromRoute] int id)
    {
        var room = _mediator.Get(id);
        var roomDTO = room.MapOne<RoomDTO>();
        var roomViewModel = roomDTO.MapOne<RoomViewModel>();

        return Ok(roomViewModel);
    }
}
