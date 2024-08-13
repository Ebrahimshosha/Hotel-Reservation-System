﻿namespace Hotel_Reservation_System.Models;

public class FeedBack : BaseModel
{
    public string Text { get; set; } = string.Empty;
    public User User { get; set; } = null!;
    public Room Room { get; set; } = null!;
    public int Rating { get; set; }
    public DateTime Submitted_at { get; set; }
}
