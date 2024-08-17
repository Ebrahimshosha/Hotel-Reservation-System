namespace Hotel_Reservation_System.Services.FacilitiesServices;

public interface IFacilitiesService
{
    Facility Get(int id);
    IEnumerable<Facility> GetFacilities();
    Facility Add(Facility facility);
    Facility Update(int id, Facility facility);
    void Delete(int id);
}
