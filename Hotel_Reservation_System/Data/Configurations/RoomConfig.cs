
namespace Hotel_Reservation_System.Data.Configurations;

public class RoomConfig : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.Property(O => O.RoomType)
               .HasConversion(

               RoomStatus => RoomStatus.ToString(),

               RoomStatus => (RoomType)Enum.Parse(typeof(RoomType), RoomStatus)

               );
    }
}
