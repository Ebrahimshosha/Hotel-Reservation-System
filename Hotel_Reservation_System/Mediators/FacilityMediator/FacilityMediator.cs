
namespace Hotel_Reservation_System.Mediators.FacilityMediator;

public class FacilityMediator : IFacilityMediator
{
    private readonly IFacilitiesService _facilitiesService;

    public FacilityMediator(IFacilitiesService facilitiesService)
    {
        _facilitiesService = facilitiesService;
    }

    public Facility Add(Facility facility)
    {
        facility = _facilitiesService.Add(facility);
        return facility;
    }

    public Facility Update(int id, Facility facility)
    {
        facility = _facilitiesService.Update(id, facility);
        return facility;
    }

    public IEnumerable<Facility> getAllFacilities()
    {
        var facilities = _facilitiesService.GetFacilities();
        return facilities;
    }

    public void DeleteFacility(int id)
    {
        _facilitiesService.Delete(id);
    }
}
