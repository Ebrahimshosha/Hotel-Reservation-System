using Hotel_Reservation_System.Consts;
using Microsoft.AspNetCore.Identity;

namespace Hotel_Reservation_System.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<ApplicationIdentityUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationIdentityUserRole> builder)
    {
        builder.HasData(new IdentityUserRole<string>
        {
            UserId = DefaultUsers.AdminId,
            RoleId = DefaultRoles.AdminRoleId
        });
    }
}