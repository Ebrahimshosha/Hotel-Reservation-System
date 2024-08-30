using Hotel_Reservation_System.DTO.Authorization;

namespace Hotel_Reservation_System.Services.Authorization;

public interface IAuthService
{
    Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
}
