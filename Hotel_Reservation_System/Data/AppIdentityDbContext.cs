

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Hotel_Reservation_System.Data;

public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
{

    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {

    }
}