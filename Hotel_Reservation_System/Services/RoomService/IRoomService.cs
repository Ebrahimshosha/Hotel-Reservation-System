using Hotel_Reservation_System.DTO.Room;

namespace Hotel_Reservation_System.Services.RoomService;

public interface IRoomService
{
    IEnumerable<RoomToReturnDto> GetAll();
    RoomToReturnDto GetById(int id);
    Task<RoomToReturnDto> AddAsync(CreateRoomDTO createRoomDTO);
    Task<RoomToReturnDto> UpdateAsync(int id, CreateRoomDTO createRoomDTO);
    bool Delete(int id);
    IEnumerable<RoomToReturnDto> GetAvailableRooms(Room room);
}
