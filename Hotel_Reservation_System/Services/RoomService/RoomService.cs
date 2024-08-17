
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

    public IEnumerable<Room> GetAll()
    {
        var rooms = _repository.GetAll();
        return rooms;
    }

    public Room Get(int id)
    {
        var room = _repository.GetByID(id);
        return room;
    }

    public async Task<Room> AddAsync(CreateRoomDTO createRoomDTO)
    {
        var fileName = await DocumentSettings.UploadFileAsync(createRoomDTO.Image_Url, "Images");

        var roomDTO = createRoomDTO.MapOne<RoomDTO>();
        roomDTO.Image_Url = fileName;
        // Is there more efficient way to mapping from CreateRoomDTO to RoomDTO in RoomMediator ?

        var room = roomDTO.MapOne<Room>();

        room = _repository.Add(room);
        _repository.SaveChanges();

        return room;
    }

    public async Task<Room> UpdateAsync(int id, CreateRoomDTO createRoomDTO)
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

        return room;
    }

    public void Delete(int id)
    {
        var room = _repository.GetByID(id);

        if (room is not null)
        {
            _repository.Delete(id);
            _repository.SaveChanges();
        }
    }
    public IEnumerable<Room> GetAvailableRooms(Room room)
    {
        var rooms = _repository.Get(x => x.Price <= room.Price && x.IsAvailable && x.RoomType == room.RoomType);
        return rooms;
    }
}
