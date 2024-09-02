namespace Hotel_Reservation_System.DTO.Authorization;

public record RegisterRequest(
    string Email,
    string Password,
    string FName,
    string LName
);
