namespace Hotel_Reservation_System.Mediators.FacilityMediator;

public interface IFacilityMediator
{
    Facility Add(Facility facility);
    IEnumerable<Facility> getAllFacilities();
    Facility Update(int id, Facility facility);
    void DeleteFacility(int id);
}
