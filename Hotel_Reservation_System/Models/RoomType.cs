using System.Runtime.Serialization;

namespace Hotel_Reservation_System.Models;

public enum RoomType
{
    [EnumMember(Value = "Single")]
    Single,

    [EnumMember(Value = "Double")]
    Double,

    [EnumMember(Value = "Triple")]
    Triple,

    [EnumMember(Value = "Suite")]
    Suite
}
