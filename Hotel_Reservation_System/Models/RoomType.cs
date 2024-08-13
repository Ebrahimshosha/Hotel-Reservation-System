using System.Runtime.Serialization;

namespace Hotel_Reservation_System.Models;

public enum RoomType
{
    [EnumMember(Value = "Single")]
    Single,

    [EnumMember(Value = "Double")]
    Double
}
