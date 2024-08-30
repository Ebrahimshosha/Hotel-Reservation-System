namespace Hotel_Reservation_System.DTO.Authorization;

public record LoginRequest(
    string Email,
    string Password
);