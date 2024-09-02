using Hotel_Reservation_System.DTO.RoomFacility;
using Hotel_Reservation_System.Models;

namespace Hotel_Reservation_System.Services.RoomFacilityService;

public interface IRoomFacilityService
{
    List<int> GetFacilityIdsByRoomId(int roomId);
    List<int> AddRoomFacility(int roomId, List<int> roomFacilityIds);
    List<int> UpdateRoomFacility(int roomId, List<int> roomFacilityIds);
    bool DeleteRoomFacilitiesByRoomId(int RoomId, List<int> FacilitiesIds = null);
}
