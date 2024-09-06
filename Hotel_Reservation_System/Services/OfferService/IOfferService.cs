using Hotel_Reservation_System.DTO.Offer;

namespace Hotel_Reservation_System.Services.OfferService
{
    public interface IOfferService
    {
        IEnumerable<Offer> GetAll();
        Offer Get(int id);
        Task<Offer> AddAsync(AddOfferDto addOfferDTO);

        Task<bool> AssignRoomsToOfferAsync(int offerId, IEnumerable<int> roomIds);
        Task<Offer> UpdateAsync(int id, EditOfferDto editOfferDTO);
        Task<bool> UpdateAssignedRoomsToOfferAsync(int offerId, IEnumerable<int> roomIds);
        void Delete(int id);

        void DeleteAssignedRooms(int offerId);

    }

}
