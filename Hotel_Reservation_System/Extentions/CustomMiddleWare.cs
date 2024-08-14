using Hotel_Reservation_System.Middlewares;

namespace Hotel_Reservation_System.Extentions;

public static class CustomMiddleWare
{
    public static IApplicationBuilder TransactionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TransactionMiddleware>();
    }
}
