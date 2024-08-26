using Hotel_Reservation_System.DTO.Offer;

namespace Hotel_Reservation_System.Services.OfferService
{
    public class OfferService : IOfferService
    {

        private readonly IRepository<Offer> _repository;

        public OfferService(IRepository<Offer> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Offer> GetAll()
        {
            var offers = _repository.GetAll();
            return offers;
        }

        public Offer Get(int id)
        {
            var offer = _repository.GetByID(id);
            return offer;
        }

        public async Task<Offer> AddAsync(AddOfferDto addOfferDTO)
        {
            // Map the DTO to the Offer entity
            var offer = addOfferDTO.MapOne<Offer>();

            // Handle the OfferRooms relationships based on RoomIds in the DTO
            foreach (var roomId in addOfferDTO.RoomIds)
            {
                offer.OfferRooms.Add(new OfferRoom
                {
                    RoomId = roomId,
                    offer = offer
                });
            }

            offer = _repository.Add(offer);
            _repository.SaveChanges();

            return offer;
        }

        public async Task<Offer> UpdateAsync(int id, AddOfferDto addOfferDTO)
        {
            var offerFromDb = _repository.GetByID(id);
            if (offerFromDb is null)
            {
                return null;
            }

            // Clear existing OfferRooms
            offerFromDb.OfferRooms.Clear();

            // Handle the OfferRooms relationships based on RoomIds in the DTO
            foreach (var roomId in addOfferDTO.RoomIds)
            {
                offerFromDb.OfferRooms.Add(new OfferRoom
                {
                    RoomId = roomId,
                    offer = offerFromDb
                });
            }

            // Map the updated data
            var offer = addOfferDTO.MapOne<Offer>();
            offer.Id = id;

            _repository.Update(offer);
            _repository.SaveChanges();

            return offer;
        }

        public void Delete(int id)
        {
            var offer = _repository.GetByID(id);

            if (offer is not null)
            {
                _repository.Delete(id);
                _repository.SaveChanges();
            }
        }


    }

}

