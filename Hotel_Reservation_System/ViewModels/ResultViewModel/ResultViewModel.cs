using Hotel_Reservation_System.Exceptions.Error;

namespace Hotel_Reservation_System.ViewModels.ResultViewModel;

public class ResultViewModel<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; } 
    public string Message { get; set; } = null!;
    public ErrorCode ErrorCode { get; set; }


    public static ResultViewModel<T> Sucess(T data, string message = "")
    {
        return new ResultViewModel<T>
        {
            IsSuccess = true,
            Data = data,
            Message = message,
            ErrorCode = ErrorCode.NoError,
        };
    }

    public static ResultViewModel<T> Faliure(ErrorCode errorCode, string message)
    {
        return new ResultViewModel<T>
        {
            IsSuccess = false,
            Data = default,
            Message = message,
            ErrorCode = errorCode,
        };
    }
}
