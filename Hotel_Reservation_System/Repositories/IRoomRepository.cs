namespace Hotel_Reservation_System.Repositories;

public interface IRoomRepository: IRepository<Room>
{
    Room GetByIDWithInclude(int id);
}
