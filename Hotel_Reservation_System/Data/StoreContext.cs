
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Hotel_Reservation_System.Data;
 
	public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
	{
		public StoreContext CreateDbContext(string[] args)
		{
			// Building configuration
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			// Building DbContextOptions
			var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

			return new StoreContext(optionsBuilder.Options);
		}
	}

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
	}

    public DbSet<Facility> Facilities { get; set; }
    public DbSet<FeedBack> FeedBacks { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<OfferRoom> offerRooms { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<FacilityRoom> RoomFacilities { get; set; }
}
