using Hotel_Reservation_System.DTO.Room;

namespace Hotel_Reservation_System.Services.RoomService;

public interface IRoomService
{
    IEnumerable<Room> GetAll();
    Room Get(int id);
    Task<Room> AddAsync(CreateRoomDTO createRoomDTO);
    Task<Room> UpdateAsync(int id, CreateRoomDTO createRoomDTO);
    void Delete(int id);
    IEnumerable<Room> GetAvailableRooms(Room room);
}
