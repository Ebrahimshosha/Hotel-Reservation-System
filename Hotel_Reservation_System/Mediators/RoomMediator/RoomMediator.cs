
namespace Hotel_Reservation_System.Mediators.RoomMediator;

public class RoomMediator : IRoomMediator
{
    private readonly IRoomService _roomService;

    public RoomMediator(IRoomService roomService)
    {
        _roomService = roomService;
    }
    public IEnumerable<RoomToReturnDto> GetAll()
    {
        var roomsToReturnDto = _roomService.GetAll();
        return roomsToReturnDto;
    }

    public RoomToReturnDto Get(int id)
    {
        var roomToReturnDto = _roomService.GetById(id);
        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> Add(CreateRoomDTO createRoomDTO)
    {
        var roomToReturnDto = await _roomService.AddAsync(createRoomDTO);
        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> Update(int id, CreateRoomDTO createRoomDTO)
    {
        var room = await _roomService.UpdateAsync(id, createRoomDTO);
        var roomToReturnDto = room.MapOne<RoomToReturnDto>();

        return roomToReturnDto;
    }

    public bool Delete(int id)
    {
        var roomToReturnDto = _roomService.GetById(id);
        if (roomToReturnDto is not null)
        {
            _roomService.Delete(id);
            return true;
        }
        return false ;
    }

    public IEnumerable<RoomDTO> ViewRoomAvailability(AvailabileRoomViewModel viewModel)
    {
        var room = viewModel.MapOne<Room>();
        var rooms = _roomService.GetAvailableRooms(room);
        var roomDTO = rooms.Select(r => r.MapOne<RoomDTO>());

        return roomDTO;
    }

}
