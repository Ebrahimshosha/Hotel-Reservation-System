using Hotel_Reservation_System.DTO.Facility;

namespace Hotel_Reservation_System.Services.FacilitiesServices;

public interface IFacilitiesService
{
    FacilityToReturnDto GetById(int id);
    IEnumerable<FacilityToReturnDto> GetFacilities();
    FacilityToReturnDto Add(FacilityDto facilityDto);
    FacilityToReturnDto Update(int id, FacilityDto facilityDto);
    bool Delete(int id);
}
