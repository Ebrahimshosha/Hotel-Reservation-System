

namespace Hotel_Reservation_System.Services.RoomService;

public class RoomService : IRoomService
{
    private readonly IRepository<Room> _repository;
    private readonly IRepository<Reservation> _reservationRepository;

    public RoomService(IRepository<Room> repository, IRepository<Reservation> ReservationRepository)
    {
        _repository = repository;
        _reservationRepository = ReservationRepository;
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


    public IEnumerable<RoomToReturnDto> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
    {

        var reservedRoomIds = GetReservedRoomIds(checkInDate, checkOutDate);

        var availableRooms = GetAvailableRooms(reservedRoomIds);

        var roomsToReturnDto = availableRooms.Map<RoomToReturnDto>();
        return roomsToReturnDto; ;
    }

    private List<int> GetReservedRoomIds(DateTime checkInDate, DateTime checkOutDate )
    {
       var reservedRoomIds = _reservationRepository.GetAll()
            .Where(r => r.Check_in_date < checkOutDate && r.Check_out_date > checkInDate)
            .Select(r => r.Room.Id)
            .Distinct()
            .ToList();

        return reservedRoomIds;
    }

    private IQueryable<Room> GetAvailableRooms(List<int> reservedRoomIds)
    {
        var availableRooms = _repository.GetAll()
                                        .Where(r => !reservedRoomIds.Contains(r.Id));

        return availableRooms;
    }
}
