namespace Hotel_Reservation_System.Models;

public class User : BaseModel
{
    public string Password { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public DateTime Registered_at { get; set; }
    public string Email { get; set; } = string.Empty;
}
