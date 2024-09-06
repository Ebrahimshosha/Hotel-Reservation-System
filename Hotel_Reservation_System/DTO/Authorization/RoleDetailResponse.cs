namespace Hotel_Reservation_System.DTO.Authorization;

public record RoleDetailResponse(
    string Id,
    string Name,
    bool IsDeleted
);