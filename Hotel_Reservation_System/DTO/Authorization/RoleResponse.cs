namespace Hotel_Reservation_System.DTO.Authorization;

public record RoleResponse(
    string Id,
    string Name,
    bool IsDeleted
);