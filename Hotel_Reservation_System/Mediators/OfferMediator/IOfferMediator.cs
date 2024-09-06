using Hotel_Reservation_System.DTO.Offer;

namespace Hotel_Reservation_System.Mediators.OfferMediator
{
    public interface IOfferMediator
    {
        IEnumerable<Offer> GetAll();
        Offer Get(int id);
        Task<OfferDTO> Add(AddOfferDto addOfferDTO);
        Task<bool> AssignRoomsToOfferAsync(int offerId, IEnumerable<int> roomIds);
        Task<OfferDTO> Update(int id, EditOfferDto editOfferDTO);
        Task<bool> UpdateAssignedRoomsToOfferAsync(int offerId, IEnumerable<int> roomIds);
        bool Delete(int id);
        bool DeleteAssignedRooms(int offerId);

    }

}
