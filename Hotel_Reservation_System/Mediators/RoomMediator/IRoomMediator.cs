
namespace Hotel_Reservation_System.Mediators.RoomMediator;

public interface IRoomMediator
{
    IEnumerable<RoomToReturnDto> GetAll();
    RoomToReturnDto GetById(int id);
    Task<RoomToReturnDto> Add(CreateRoomDTO createRoomDTO);
    Task<RoomToReturnDto> Update(int id, CreateRoomDTO createRoomDTO);
    bool Delete(int id);
    IEnumerable<RoomDTO> ViewRoomAvailability(AvailabileRoomViewModel availabileRoomViewModel);
}
