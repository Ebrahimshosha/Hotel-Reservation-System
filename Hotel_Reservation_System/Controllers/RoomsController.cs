
using Hotel_Reservation_System.DTO.Room;
using Hotel_Reservation_System.Exceptions.Error;
using Hotel_Reservation_System.ViewModels.CreateImagesViewModel;
using Hotel_Reservation_System.ViewModels.ResultViewModel;
using Hotel_Reservation_System.ViewModels.Room;

namespace Hotel_Reservation_System.Controllers;

public class RoomsController : BaseApiController
{
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

    [HttpPut("{id}")]
    public async Task<ResultViewModel<RoomViewModel>> UpdateRoom([FromRoute] int id, [FromForm] CreateRoomViewModel viewModel)
    {
        var createRoomDTO = viewModel.MapOne<CreateRoomDTO>();
        var roomToreturnDTO = await _mediator.Update(id, createRoomDTO);
        var roomViewModel = roomToreturnDTO.MapOne<RoomViewModel>();

        return ResultViewModel<RoomViewModel>.Sucess(roomViewModel);
    }

    [HttpPost("UpdateRoomFacilities/{RoomId}")]
    public async Task<ResultViewModel<bool>> UpdateRoomFacilities([FromRoute] int RoomId, [FromBody] CreateFacilityViewModel viewModel)
    {
        var roomToreturnDTO = await _mediator.UpdateRoomFacilities(RoomId, viewModel);

        return ResultViewModel<bool>.Sucess(true, $"Facilities in Room {RoomId} is Updated");
    }

    [HttpPost("UpdateRoomImages/{RoomId}")]
    public async Task<ResultViewModel<bool>> UpdateRoomImages([FromRoute] int RoomId, [FromRoute] CreateImagesViewModel viewModel)
    {
        var roomToReturnDto = await _mediator.UpdateRoomImages(RoomId, viewModel);

        return ResultViewModel<bool>.Sucess(true, $"Images in Room {RoomId} is Updated");
    }
    [HttpPost("DeleteRoomFacilities/{RoomId}")]
    public async Task<ResultViewModel<bool>> DeleteRoomFacilities([FromRoute] int RoomId, [FromBody] CreateFacilityViewModel viewModel)
    {
        var roomToreturnDTO = await _mediator.DeleteRoomFacilities(RoomId, viewModel);

        return ResultViewModel<bool>.Sucess(true, $"Facilities in Room {RoomId} is Updated");
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
