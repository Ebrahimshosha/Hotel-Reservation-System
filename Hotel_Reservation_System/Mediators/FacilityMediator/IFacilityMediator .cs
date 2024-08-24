using Hotel_Reservation_System.DTO.Facility;
using Hotel_Reservation_System.ViewModels.FacilitiesViewModel;

namespace Hotel_Reservation_System.Mediators.FacilityMediator;

public interface IFacilityMediator
{
    IEnumerable<FacilityToReturnDto> getAllFacilities();
    FacilityToReturnDto GetById(int id);
    FacilityToReturnDto Add(CreateFacilityViewModel viewModel);
    FacilityToReturnDto Update(int id, CreateFacilityViewModel viewModel);
    bool DeleteFacility(int id);
}
