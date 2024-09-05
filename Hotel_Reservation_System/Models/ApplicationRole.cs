using Microsoft.AspNetCore.Identity;

namespace Hotel_Reservation_System.Models;

public class ApplicationRole : IdentityRole
{
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
}