
using AutoMapper.QueryableExtensions;
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

    public async Task<RoomToReturnDto> AddAsync(CreateRoomDTO createRoomDTO)
    {
        var fileName = await DocumentSettings.UploadFileAsync(createRoomDTO.Image_Url, "Images");

        var roomDTO = createRoomDTO.MapOne<RoomDTO>();
        roomDTO.Image_Url = fileName;
        // Is there more efficient way to mapping from CreateRoomDTO to RoomDTO in RoomMediator ?

        var room = roomDTO.MapOne<Room>();

        room = _repository.Add(room);
        _repository.SaveChanges();

        var roomToReturnDto = room.MapOne<RoomToReturnDto>();
        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> UpdateAsync(int id, CreateRoomDTO createRoomDTO)
    {
        var roomfromdb = _repository.GetByID(id);
        if (roomfromdb is null)
        {
            return null;
        }
        var fileName = await DocumentSettings.UploadFileAsync(createRoomDTO.Image_Url, "Images");

        var roomDTO = createRoomDTO.MapOne<RoomDTO>();
        roomDTO.Image_Url = fileName;
        // Is there more efficient way to mapping from CreateRoomDTO to RoomDTO in RoomMediator ?

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
        var rooms = _repository.Get(x => x.Price <= room.Price && x.IsAvailable && x.RoomType == room.RoomType);

        var roomToReturnDto = rooms.Select(r => r.MapOne<RoomToReturnDto>());
        return roomToReturnDto;
    }
}
