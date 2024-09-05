
using Hotel_Reservation_System.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Hotel_Reservation_System.Data;

public class AppIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfiguration<ApplicationUser>(new UserConfiguration());
        modelBuilder.ApplyConfiguration<ApplicationRole>(new RoleConfiguration());
        modelBuilder.ApplyConfiguration<ApplicationIdentityUserRole>(new UserRoleConfiguration());

    }
    

}