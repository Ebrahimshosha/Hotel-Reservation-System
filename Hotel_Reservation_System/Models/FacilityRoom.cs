using Autofac.Features.GeneratedFactories;

namespace Hotel_Reservation_System.Models;

public class FacilityRoom : BaseModel
{
    public int FacilityId { get; set; } 
    public Facility Facility { get; set; } = null!;
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
}
