using Autofac.Features.GeneratedFactories;

namespace Hotel_Reservation_System.Models;

public class FacilityRoom : BaseModel
{
    public int FacilitiesId { get; set; } 
    public Facility Facilities { get; set; } = null!;
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
}
