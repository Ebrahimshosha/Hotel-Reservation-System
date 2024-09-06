
using Hotel_Reservation_System.DTO.Room;
using Hotel_Reservation_System.Models;
using Hotel_Reservation_System.Services.ReservationService;
using Hotel_Reservation_System.ViewModels.CreateImagesViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace Hotel_Reservation_System.Mediators.RoomMediator;

public class RoomMediator : IRoomMediator
{
    private readonly IRoomService _roomService;
    private readonly IRoomImagesServices _roomImagesServices;
    private readonly IRoomFacilityService _roomFacilityService;
    private readonly IReservationService _reservationService;

    public RoomMediator
    (
        IRoomService roomService,
        IRoomImagesServices roomImagesServices,
        IRoomFacilityService roomFacilityService,
        IReservationService reservationService
    )
    {
        _roomService = roomService;
        _roomImagesServices = roomImagesServices;
        _roomFacilityService = roomFacilityService;
        _reservationService = reservationService;
    }
    public IEnumerable<RoomToReturnDto> GetAll()
    {
        var roomsToReturnDto = _roomService.GetAll();
        return roomsToReturnDto;
    }

    public RoomToReturnDto GetById(int id)
    {
        var roomToReturnDto = _roomService.GetById(id);

        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> Add(CreateRoomDTO createRoomDTO)
    {
        var roomDTO = createRoomDTO.MapOne<RoomDTO>();

        var roomToReturnDto = await _roomService.AddAsync(roomDTO);

        //var images = await _roomImagesServices.AddImagesRoom(roomToReturnDto.Id, createRoomDTO.Images);
        //roomToReturnDto.Images = images;

        //var facilitiesIds = _roomFacilityService.AddRoomFacility(roomToReturnDto.Id, createRoomDTO.FacilitiesIds);
        //roomToReturnDto.FacilitiesIds = facilitiesIds;

        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> Update(int id, CreateRoomDTO createRoomDTO)
    {
        var roomDTO = createRoomDTO.MapOne<RoomDTO>();

        var roomToReturnDto = await _roomService.UpdateAsync(id, roomDTO);


        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> UpdateRoomFacilities(int RoomId, CreateFacilityViewModel viewModel)
    {
        var roomToReturnDto = _roomService.GetById(RoomId);
        var existingFacilitiesIds = roomToReturnDto.FacilitiesIds;

        var addedFacilityIds = viewModel.FacilitiesIds.Except(existingFacilitiesIds).ToList();

        var facilitiesIds = _roomFacilityService.AddRoomFacility(RoomId, addedFacilityIds);
        roomToReturnDto.FacilitiesIds = existingFacilitiesIds.Concat(facilitiesIds).Distinct().ToList();

        return roomToReturnDto;
    }

    public async Task<RoomToReturnDto> UpdateRoomImages(int RoomId, CreateImagesViewModel viewModel)
    {
        var roomToReturnDto = _roomService.GetById(RoomId);
        var facilitiesImages = await _roomImagesServices.AddImagesRoom(RoomId, viewModel.Images);

        roomToReturnDto.Images = roomToReturnDto.Images.Concat(facilitiesImages).ToList();

        return roomToReturnDto;
    }

    public bool Delete(int id)
    {
        var roomToReturnDto = _roomService.GetById(id);
        if (roomToReturnDto is not null)
        {
            _roomService.Delete(id);
            _roomFacilityService.DeleteRoomFacilitiesByRoomId(id);
            _roomImagesServices.DeleteRoomImages(id);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteRoomFacilities(int RoomId, CreateFacilityViewModel viewModel)
    {
        var facilitiesIds = _roomFacilityService.DeleteRoomFacilitiesByRoomId(RoomId, viewModel.FacilitiesIds);

        return true;
    }
    public async Task<bool>  DeleteRoomImages(int RoomId,List<string> Images)
    {
        var images = _roomImagesServices.DeleteRoomImages(RoomId, Images);

        return true;
    }

    public IEnumerable<RoomToReturnDto> ViewRoomAvailability(DateTime checkInDate, DateTime checkOutDate)
    {
        var roomsToReturnDto = _reservationService.GetAvailableRooms(checkInDate, checkOutDate);
        return roomsToReturnDto;
    }
}
