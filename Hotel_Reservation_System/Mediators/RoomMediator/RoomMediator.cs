
namespace Hotel_Reservation_System.Mediators.RoomMediator;

public class RoomMediator : IRoomMediator
{
    private readonly IRoomService _roomService;
    private readonly IRoomImagesServices _roomImagesServices;
    private readonly IRoomFacilityService _roomFacilityService;

    public RoomMediator
    (
        IRoomService roomService,
        IRoomImagesServices roomImagesServices,
        IRoomFacilityService roomFacilityService
    )
    {
        _roomService = roomService;
        _roomImagesServices = roomImagesServices;
        _roomFacilityService = roomFacilityService;
    }
    public IEnumerable<RoomToReturnDto> GetAll()
    {
        var roomsToReturnDto = _roomService.GetAll();
        return roomsToReturnDto;
    }

    public RoomToReturnDto GetById(int id)
    {
        var roomToReturnDto = _roomService.GetById(id);

        var facilitiesIds = _roomFacilityService.GetFacilityIdsByRoomId(id);
        roomToReturnDto.FacilitiesIds = facilitiesIds;

        var images =  _roomImagesServices.GetImageUrlsByRoomId(id);
        roomToReturnDto.images = images;

        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> Add(CreateRoomDTO createRoomDTO)
    {
        var roomDTO = createRoomDTO.MapOne<RoomDTO>();

        var roomToReturnDto = await _roomService.AddAsync(roomDTO);

        var images = await _roomImagesServices.AddImagesRoom(roomToReturnDto.Id, createRoomDTO.Images);
        roomToReturnDto.images = images;

        var facilitiesIds = _roomFacilityService.AddRoomFacility(roomToReturnDto.Id,createRoomDTO.FacilitiesIds);
        roomToReturnDto.FacilitiesIds = facilitiesIds;

        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> Update(int id, CreateRoomDTO createRoomDTO)
    {
        var roomDTO = createRoomDTO.MapOne<RoomDTO>();

        var roomToReturnDto = await _roomService.UpdateAsync(id, roomDTO);

        var images = await _roomImagesServices.UpdateImagesRoom(roomToReturnDto.Id, createRoomDTO.Images);
        roomToReturnDto.images = images;

        var facilitiesIds = _roomFacilityService.UpdateRoomFacility(roomToReturnDto.Id, createRoomDTO.FacilitiesIds);
        roomToReturnDto.FacilitiesIds = facilitiesIds;

        return roomToReturnDto;
    }

    public bool Delete(int id)
    {
        var roomToReturnDto = _roomService.GetById(id);
        if (roomToReturnDto is not null)
        {
            _roomService.Delete(id);
            _roomFacilityService.DeleteRoomFacilitiesByRoomId(id);
            _roomImagesServices.DeleteRoomImagesByRoomId(id);
            return true;
        }
        return false;
    }

    public IEnumerable<RoomDTO> ViewRoomAvailability(AvailabileRoomViewModel viewModel)
    {
        var room = viewModel.MapOne<Room>();
        var rooms = _roomService.GetAvailableRooms(room);
        var roomDTO = rooms.Select(r => r.MapOne<RoomDTO>());

        return roomDTO;
    }
}
