
namespace Hotel_Reservation_System.Mediators.RoomMediator;

public class RoomMediator : IRoomMediator
{
    private readonly IRoomService _roomService;

    public RoomMediator(IRoomService roomService)
    {
        _roomService = roomService;
    }
    public IEnumerable<Room> GetAll()
    {
        var rooms = _roomService.GetAll();
        return rooms;
    }

    public Room Get(int id)
    {
        var room = _roomService.Get(id);
        return room;
    }

    public async Task<RoomDTO> Add(CreateRoomDTO createRoomDTO)
    {
        var room = await _roomService.AddAsync(createRoomDTO);
        var roomDTO = room.MapOne<RoomDTO>();

        return roomDTO;
    }

    public async Task<RoomDTO> Update(int id, CreateRoomDTO createRoomDTO)
    {
        var room = await _roomService.UpdateAsync(id, createRoomDTO);
        var roomDTO = room.MapOne<RoomDTO>();

        return roomDTO;
    }

    public void Delete(int id)
    {
        _roomService.Delete(id);
    }

    public IEnumerable<RoomDTO> ViewRoomAvailability(AvailabileRoomViewModel viewModel)
    {
        var room = viewModel.MapOne<Room>();
        var rooms = _roomService.GetAvailableRooms(room);
        var roomDTO = rooms.Select(r => r.MapOne<RoomDTO>());

        return roomDTO;
    }

}
