using Hotel_Reservation_System.Exceptions.Error;
using Hotel_Reservation_System.Exceptions;
using Hotel_Reservation_System.ViewModels.ResultViewModel;
using System.Net;
using System.Text.Json;

namespace Hotel_Reservation_System.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

    public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error happened : {ex.Message}");

            context.Response.ContentType = "applicatin/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Error Occured -- InternalServerError";
            ErrorCode errorCode = ErrorCode.InternalserverError;

            if (ex is BusinessException businessException)
            {
                message = businessException.Message;
                errorCode = businessException.ErrorCode;
            }
            else
            {
                File.WriteAllText("F:\\Log.txt", $"Error happened: {ex.Message},{ex.StackTrace!.ToString()}");
            }

            var result = ResultViewModel<bool>.Faliure(errorCode, message);

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var JsonResponse = JsonSerializer.Serialize(result, options);
            await context.Response.WriteAsJsonAsync(JsonResponse);
        }
    }
}
