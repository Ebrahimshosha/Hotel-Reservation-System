using Hotel_Reservation_System.DTO.Offer;

namespace Hotel_Reservation_System.Services.OfferService
{
    public interface IOfferService
    {
        IEnumerable<Offer> GetAll();
        Offer Get(int id);
        Task<Offer> AddAsync(AddOfferDto addOfferDTO);
        Task<Offer> UpdateAsync(int id, AddOfferDto addOfferDTO);
        void Delete(int id);

    }

}
