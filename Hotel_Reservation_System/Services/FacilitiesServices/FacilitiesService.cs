
namespace Hotel_Reservation_System.Services.FacilitiesServices;

public class FacilitiesService : IFacilitiesService
{
    private readonly IRepository<Facility> _repository;

    public FacilitiesService(IRepository<Facility> repository)
    {
        _repository = repository;
    }

    public Facility Get(int id)
    {
        var facility = _repository.GetByID(id);
        return facility;
    }
    public IEnumerable<Facility> GetFacilities()
    {
        var facilities = _repository.GetAll();
        return facilities;
    }
    public Facility Add(Facility facility)
    {
        facility = _repository.Add(facility);
        return facility;
    }
    public Facility Update(int id, Facility facility)
    {
        facility = _repository.GetWithTrackinByID(id);

        facility.Id = id;
        facility.Name = facility.Name;
        facility.price = facility.price;

        _repository.Update(facility);
        _repository.SaveChanges();

        return facility;
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
}
