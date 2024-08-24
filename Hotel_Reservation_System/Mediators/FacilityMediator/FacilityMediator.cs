
using Hotel_Reservation_System.DTO.Facility;
using Hotel_Reservation_System.ViewModels.FacilitiesViewModel;

namespace Hotel_Reservation_System.Mediators.FacilityMediator;

public class FacilityMediator : IFacilityMediator
{
    private readonly IFacilitiesService _facilitiesService;

    public FacilityMediator(IFacilitiesService facilitiesService)
    {
        _facilitiesService = facilitiesService;
    }

    public FacilityToReturnDto Add(CreateFacilityViewModel viewModel)
    {
        var facilityDTO = viewModel.MapOne<FacilityDto>();

        var facilityToReturnDto = _facilitiesService.Add(facilityDTO);

        return facilityToReturnDto;
    }

    public FacilityToReturnDto Update(int id, CreateFacilityViewModel  viewModel)
    {
        var facilityDTO = viewModel.MapOne<FacilityDto>();

        var facilityToReturnDto = _facilitiesService.Update(id, facilityDTO);
        
        return facilityToReturnDto;
    } 
    
    public FacilityToReturnDto GetById(int id)
    {
        var facilityToReturnDto = _facilitiesService.GetById(id);
        return facilityToReturnDto;
    }

    public IEnumerable<FacilityToReturnDto> getAllFacilities()
    {
        var facilitiesToReturnDto = _facilitiesService.GetFacilities();
        return facilitiesToReturnDto;
    }

    public bool DeleteFacility(int id)
    {
        var facilty  = _facilitiesService.GetById(id);
        if (facilty is not null)
        {
            _facilitiesService.Delete(id);
            return true;
        }
        return false;
    }
}
