namespace Hotel_Reservation_System.Services.Authorization;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user);
}