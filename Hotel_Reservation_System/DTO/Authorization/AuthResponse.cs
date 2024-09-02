namespace Hotel_Reservation_System.DTO.Authorization;

public record AuthResponse(
    string Id,
    string? Email,
    string FirstName,
    string LastName,
    string Token,
    int ExpiresIn
);
