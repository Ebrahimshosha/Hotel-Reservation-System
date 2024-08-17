
namespace Hotel_Reservation_System.Mediators.RoomMediator;

public interface IRoomMediator
{
    IEnumerable<Room> GetAll();
    Room Get(int id);
    Task<RoomDTO> Add(CreateRoomDTO createRoomDTO);
    Task<RoomDTO> Update(int id, CreateRoomDTO createRoomDTO);
    void Delete(int id);
    IEnumerable<RoomDTO> ViewRoomAvailability(AvailabileRoomViewModel availabileRoomViewModel);
}
