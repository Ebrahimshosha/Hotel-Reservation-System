
namespace Hotel_Reservation_System.Data.Configurations;

public class ReservationConfig : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(R => R.ReservationStatus)
              .HasConversion(

              Status => Status.ToString(),

              Status => (ReservationStatus)Enum.Parse(typeof(ReservationStatus), Status)

              );
    }
}
