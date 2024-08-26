using Hotel_Reservation_System.DTO.Offer;

namespace Hotel_Reservation_System.Mediators.OfferMediator
{
    public interface IOfferMediator
    {
        IEnumerable<Offer> GetAll();
        Offer Get(int id);
        Task<OfferDTO> Add(AddOfferDto addOfferDTO);
        Task<OfferDTO> Update(int id, EditOfferDto editOfferDTO);
        bool Delete(int id);

    }

}
