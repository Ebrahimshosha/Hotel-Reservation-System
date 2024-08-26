
using Hotel_Reservation_System.ViewModels.CreateImagesViewModel;

namespace Hotel_Reservation_System.Mediators.RoomMediator;

public interface IRoomMediator
{
    IEnumerable<RoomToReturnDto> GetAll();
    RoomToReturnDto GetById(int id);
    Task<RoomToReturnDto> Add(CreateRoomDTO createRoomDTO);
    Task<RoomToReturnDto> UpdateRoomFacilities(int RoomId, CreateFacilityViewModel viewModel);
    Task<RoomToReturnDto> UpdateRoomImages(int RoomId, CreateImagesViewModel viewModel);
    Task<RoomToReturnDto> Update(int id, CreateRoomDTO createRoomDTO);
    bool Delete(int id);
    IEnumerable<RoomToReturnDto> ViewRoomAvailability(DateTime checkInDate, DateTime checkOutDate);
    Task<bool> DeleteRoomFacilities(int RoomId, CreateFacilityViewModel viewModel);
    Task<bool> DeleteRoomImages(int RoomId, List<string> Images);
}
