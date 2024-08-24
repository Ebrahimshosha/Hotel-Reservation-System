
using Hotel_Reservation_System.ViewModels.CreateImagesViewModel;

namespace Hotel_Reservation_System.Mediators.RoomMediator;

public interface IRoomMediator
{
    IEnumerable<RoomToReturnDto> GetAll();
    RoomToReturnDto GetById(int id);
    Task<RoomToReturnDto> Add(CreateRoomDTO createRoomDTO);
    Task<RoomToReturnDto> AddFacilitiesToRoom(int RoomId, CreateFacilityViewModel viewModel);
    Task<RoomToReturnDto> AddImagesToRoom(int RoomId, CreateImagesViewModel viewModel);
    Task<RoomToReturnDto> Update(int id, CreateRoomDTO createRoomDTO);
    bool Delete(int id);
    IEnumerable<RoomToReturnDto> ViewRoomAvailability(DateTime checkInDate, DateTime checkOutDate);
}
