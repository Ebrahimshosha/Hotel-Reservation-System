using Autofac.Features.GeneratedFactories;

namespace Hotel_Reservation_System.Models;

public class RoomFacility:BaseModel
{
    public int FacilitiesId { get; set; } 
    public Facilities Facilities { get; set; } = null!;
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
}
