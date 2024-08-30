
namespace Hotel_Reservation_System.Exceptions.Error;

public enum ErrorCode
{
    NoError = 0,
    BadRequest = 400,
    UareNotAuthorized = 401,
    ResourceNotFound = 404, 
    InternalserverError = 500,
    TokenGenerationError = 600

}
