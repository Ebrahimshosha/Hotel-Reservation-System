namespace Hotel_Reservation_System.ViewModels.FacilitiesViewModel;

public class CreateFacilityViewModel
{
    public string Name { get; set; } = string.Empty;
    public double price { get; set; }
    public List<int> FacilitiesIds { get; set; }
}
