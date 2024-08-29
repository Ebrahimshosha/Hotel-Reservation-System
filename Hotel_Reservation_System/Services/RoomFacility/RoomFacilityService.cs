
using Hotel_Reservation_System.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Hotel_Reservation_System.Services.RoomFacilityService;

public class RoomFacilityService : IRoomFacilityService
{
    private readonly IRepository<FacilityRoom> _repository;

    public RoomFacilityService(IRepository<FacilityRoom> repository)
    {
        _repository = repository;
    }

    public List<int> GetFacilityIdsByRoomId(int roomId)
    {
        return _repository.Get(rf => rf.RoomId == roomId).Select(rf => rf.FacilityId).ToList();
    }

    public List<int> AddRoomFacility(int roomId, List<int> roomFacilityIds)
    {
        foreach (var id in roomFacilityIds)
        {
            _repository.Add(new FacilityRoom { RoomId = roomId, FacilityId = id });
        }
        _repository.SaveChanges();

        return roomFacilityIds;
    }

    public List<int> UpdateRoomFacility(int roomId, List<int> roomFacilityIds)
    {
        var existingRoomFacilities = _repository.Get(r => r.RoomId == roomId).ToList();

        foreach (var roomFacility in existingRoomFacilities)
        {
            _repository.Delete(roomFacility.Id);
        }
        _repository.SaveChanges();

        var updatedRoomFacilityids = AddRoomFacility(roomId, roomFacilityIds);
        return updatedRoomFacilityids;
    }

    public bool DeleteRoomFacilitiesByRoomId(int RoomId, List<int> FacilitiesIds = null)
    {
        bool hasFacilitiesIds = FacilitiesIds != null && FacilitiesIds.Count > 0;

        var roomFacilities = _repository.Get(r => r.RoomId == RoomId && (!hasFacilitiesIds || FacilitiesIds!.Contains(r.FacilityId))).ToList();

        if (roomFacilities.Count == 0) { return false; }

        foreach (var roomFacility in roomFacilities)
        {
            _repository.Delete(roomFacility.Id);
        }

        _repository.SaveChanges();

        return true;
    }
}
