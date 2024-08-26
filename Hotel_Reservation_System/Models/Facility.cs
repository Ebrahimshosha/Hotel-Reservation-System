namespace Hotel_Reservation_System.Models;

public class Facility : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public double price { get; set; }
    public ICollection<FacilityRoom> FacilityRooms { get; set; } = new HashSet<FacilityRoom>();
}
