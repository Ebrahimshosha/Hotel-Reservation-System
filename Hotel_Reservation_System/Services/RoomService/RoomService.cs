
using AutoMapper.QueryableExtensions;
using Hotel_Reservation_System.Data.migirations;
using Hotel_Reservation_System.DTO.Room;
using Hotel_Reservation_System.Helpers;

namespace Hotel_Reservation_System.Services.RoomService;

public class RoomService : IRoomService
{
    private readonly IRepository<Room> _repository;

    public RoomService(IRepository<Room> repository)
    {
        _repository = repository;
    }

    public IEnumerable<RoomToReturnDto> GetAll()
    {
        var rooms = _repository.GetAll();
        var roomsToReturnDto = rooms.Select(r => r.MapOne<RoomToReturnDto>());
        return roomsToReturnDto;
    }

    public RoomToReturnDto GetById(int id)
    {
        var room = _repository.GetByID(id);
        var roomToReturnDto = room.MapOne<RoomToReturnDto>();
        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> AddAsync(RoomDTO roomDTO)
    {
        var room = roomDTO.MapOne<Room>();

        room = _repository.Add(room);
        _repository.SaveChanges();

        var roomToReturnDto =  room.MapOne<RoomToReturnDto>();
        return  roomToReturnDto;
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
    
    
    public IEnumerable<RoomToReturnDto> GetAvailableRooms(Room room)
    {

        return null;
    }

   
}
