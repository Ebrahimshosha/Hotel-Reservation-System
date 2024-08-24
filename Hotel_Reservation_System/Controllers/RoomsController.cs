
using Hotel_Reservation_System.DTO.Room;
using Hotel_Reservation_System.Exceptions.Error;
using Hotel_Reservation_System.ViewModels.CreateImagesViewModel;
using Hotel_Reservation_System.ViewModels.ResultViewModel;
using Hotel_Reservation_System.ViewModels.Room;

namespace Hotel_Reservation_System.Controllers;

public class RoomsController : BaseApiController
{
    // edit
    private readonly IRoomMediator _mediator;

    public RoomsController(IRoomMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public ResultViewModel<IEnumerable<RoomViewModel>> GetAllRoom()
    {
        var roomsToreturnDto = _mediator.GetAll();
        var roomsViewModel = roomsToreturnDto.Select(r => r.MapOne<RoomViewModel>());

        return ResultViewModel<IEnumerable<RoomViewModel>>.Sucess(roomsViewModel);
    }

    [HttpGet("{id}")]
    public ResultViewModel<RoomViewModel> GetRoomById([FromRoute] int id)
    {
        var roomToreturnDto = _mediator.GetById(id);
        var roomViewModel = roomToreturnDto.MapOne<RoomViewModel>();

        return ResultViewModel<RoomViewModel>.Sucess(roomViewModel);
    }

    [HttpPost("")]
    public async Task<ResultViewModel<RoomViewModel>> AddRoom([FromForm] CreateRoomViewModel viewModel)
    {
        var createRoomDTO = viewModel.MapOne<CreateRoomDTO>();
        var roomToreturnDTO = await _mediator.Add(createRoomDTO);
        var roomViewModel = roomToreturnDTO.MapOne<RoomViewModel>();

        return ResultViewModel<RoomViewModel>.Sucess(roomViewModel);
    }

    [HttpPost("AddFacilitiesToRoom/{RoomId}")]
    public async Task<ResultViewModel<RoomViewModel>> AddFacilitiesToRoom([FromRoute] int RoomId, [FromBody] CreateFacilityViewModel viewModel)
    {
        var roomToreturnDTO = await _mediator.AddFacilitiesToRoom(RoomId, viewModel);

        var roomViewModel = roomToreturnDTO.MapOne<RoomViewModel>();

        return ResultViewModel<RoomViewModel>.Sucess(roomViewModel);
    }

    [HttpPost("AddImagesToRoom/{RoomId}")]
    public async Task<ResultViewModel<bool>> AddImagesToRoom([FromRoute] int RoomId, [FromRoute] CreateImagesViewModel viewModel)
    {
        var images = await _mediator.AddImagesToRoom(RoomId, viewModel);

        return ResultViewModel<bool>.Sucess(true);
    }


    [HttpPut("{id}")]
    public async Task<ResultViewModel<RoomViewModel>> UpdateRoom([FromRoute] int id, [FromForm] CreateRoomViewModel viewModel)
    {
        var createRoomDTO = viewModel.MapOne<CreateRoomDTO>();
        var roomToreturnDTO = await _mediator.Update(id, createRoomDTO);
        var roomViewModel = roomToreturnDTO.MapOne<RoomViewModel>();

        return ResultViewModel<RoomViewModel>.Sucess(roomViewModel);
    }

    

    [HttpDelete("{id}")]
    public ResultViewModel<bool> DeleteRoom([FromRoute] int id)
    {
        var isDeleted = _mediator.Delete(id);

        if (isDeleted)
        {
            return ResultViewModel<bool>.Sucess(true, "Room is Deleted");
        }

        return ResultViewModel<bool>.Faliure(ErrorCode.ResourceNotFound, "Room is not existed");
    }

    [HttpGet("ViewRoomAvailability")]
    public ResultViewModel<IEnumerable<RoomViewModel>> ViewRoomAvailability(DateTime checkInDate, DateTime checkOutDate)
    {
        var roomsToreturnDto = _mediator.ViewRoomAvailability(checkInDate, checkOutDate);
        var roomsViewModel = roomsToreturnDto.Select(r => r.MapOne<RoomViewModel>());

        return ResultViewModel<IEnumerable<RoomViewModel>>.Sucess(roomsViewModel);
    }
}
