
using Hotel_Reservation_System.DTO.Facility;
using Microsoft.IdentityModel.Tokens;

namespace Hotel_Reservation_System.Services.FacilitiesServices;

public class FacilitiesService : IFacilitiesService
{
    private readonly IRepository<Facility> _repository;

    public FacilitiesService(IRepository<Facility> repository)
    {
        _repository = repository;
    }

    public FacilityToReturnDto GetById(int id)
    {
        var facility = _repository.GetByID(id);
        var facilityToReturnDto = facility.MapOne<FacilityToReturnDto>();
        return facilityToReturnDto;
    }
    public IEnumerable<FacilityToReturnDto> GetFacilities()
    {
        var facilities = _repository.GetAll();
        var facilitiesToReturnDto = facilities.Select(f => f.MapOne<FacilityToReturnDto>());
        return facilitiesToReturnDto;
    }

    public FacilityToReturnDto Add(FacilityDto facilityDTO)
    {
        var facilit = facilityDTO.MapOne<Facility>();

        var facility = _repository.Add(facilit);
        _repository.SaveChanges();

        var facilityToReturnDto = facility.MapOne<FacilityToReturnDto>();
        return facilityToReturnDto;
    }

    public FacilityToReturnDto Update(int id, FacilityDto facilityDTO)
    {
        var facilityfromdb = _repository.GetByID(id);

        if (facilityfromdb is not null)
        {
            var facility = facilityDTO.MapOne<Facility>();

            facility.Id = id;
            facility.Name = facilityDTO.Name;
            facility.price = facilityDTO.price;

            _repository.Update(facility);
            _repository.SaveChanges();

            var facilityToReturnDto = facility.MapOne<FacilityToReturnDto>();

            return facilityToReturnDto;
        }
        return null;
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
}
