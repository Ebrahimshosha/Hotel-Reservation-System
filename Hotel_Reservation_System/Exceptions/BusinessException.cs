
using Hotel_Reservation_System.Exceptions.Error;

namespace Hotel_Reservation_System.Exceptions;

public class BusinessException : Exception
{
    public ErrorCode ErrorCode { get; set; }

    public BusinessException(ErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}
