

using Hotel_Reservation_System.Exceptions;
using Hotel_Reservation_System.Exceptions.Error;

namespace Hotel_Reservation_System.Services.RoomService;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;

    public RoomService(IRoomRepository _repository)
    {
        this._repository = _repository;
    }

    public IEnumerable<RoomToReturnDto> GetAll()
    {
        var rooms = _repository.GetAll();
        var roomsToReturnDto = rooms.Map<RoomToReturnDto>();
        return roomsToReturnDto;
    }

    public RoomToReturnDto GetById(int id)
    {
        var rooms = _repository.Get(r => r.Id == id);
        var roomToReturnDto = rooms.Map<RoomToReturnDto>().FirstOrDefault()!;
        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> AddAsync(RoomDTO roomDTO)
    {
        var room = roomDTO.MapOne<Room>();
        room = _repository.Add(room);
        _repository.SaveChanges();

        var roomToReturnDto = room.MapOne<RoomToReturnDto>();
        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> UpdateAsync(int id, RoomDTO roomDTO)
    {
        var roomfromdb = _repository.GetByID(id);
        if (roomfromdb is null)
        {
            return null;
        }

        var room = roomDTO.MapOne<Room>();
        room.Id = id;

        _repository.Update(room);
        _repository.SaveChanges();

        var roomToReturnDto = room.MapOne<RoomToReturnDto>();
        return roomToReturnDto;
    }

    public bool Delete(int id)
    {
        var room = _repository.GetByID(id);

        if (room is not null)
        {
            _repository.Delete(id);
            _repository.SaveChanges();
            return true;
        }
        return false;
    }
    public double CalculateTotalPrice(int roomId)
    {
        Room room = _repository.GetByIDWithInclude(roomId);

        if (room == null)
        {
            throw new BusinessException(ErrorCode.ResourceNotFound, "Room is not found");
        }

        double totalPrice = room.Price;
        var Roomfacilities = room.FacilityRooms.Where(fe=>!fe.IsDeleted).ToList();
        foreach (var facilityRoom in Roomfacilities)
        {
            totalPrice += facilityRoom.Facility.price;
        }

        return totalPrice;
    }
}
