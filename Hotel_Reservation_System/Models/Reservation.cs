namespace Hotel_Reservation_System.Models;

public class Reservation : BaseModel
{
    public DateTime Check_in_date { get; set; }
    public DateTime Check_out_date { get; set; }
    public double Total_Price { get; set; }
    public Room Room { get; set; } = null!;

	//(One to Many)
	//public ICollection<Room> RoomList { get; set; } = new List<Room>();

	public User User { get; set; } = null!;
	public IEnumerable<object> RoomList { get; internal set; }
}
