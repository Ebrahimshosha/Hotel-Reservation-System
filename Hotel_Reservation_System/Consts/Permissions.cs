namespace Hotel_Reservation_System.Consts;

public static class Permissions
{
    public static string Type { get; } = "permissions";

    // All premessions ...
    public static IList<string?> GetAllPermissions() =>
        typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList();
}