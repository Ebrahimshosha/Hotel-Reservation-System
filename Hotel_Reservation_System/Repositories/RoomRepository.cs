namespace Hotel_Reservation_System.Repositories;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    private readonly StoreContext _context;

    public RoomRepository(StoreContext context) : base(context)
    {
        _context = context;
    }
    

    public Room GetByIDWithInclude(int id)
    {
        return _context.Set<Room>()
                       .Include(r => r.FacilityRooms)
                       .ThenInclude(r => r.Facility)
                       .FirstOrDefault(x => x.Id == id && !x.IsDeleted)!;
    }
}
