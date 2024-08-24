namespace Hotel_Reservation_System.Extentions;

public static class CustomGlobalErrorHandlerMiddleware
{
    public static IApplicationBuilder GlobalErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalErrorHandlerMiddleware>();
    }
}
