
namespace Hotel_Reservation_System.Models;

public class BaseModel
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; } = false;
}
