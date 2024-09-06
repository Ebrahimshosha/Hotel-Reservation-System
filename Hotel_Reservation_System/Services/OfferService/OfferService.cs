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
            var offer = addOfferDTO.MapOne<Offer>();
            offer = _repository.Add(offer);
            _repository.SaveChanges();
            return offer;
        }

        public async Task<bool> AssignRoomsToOfferAsync(int offerId, IEnumerable<int> roomIds)
        {
            // Retrieve the offer from the database
            var offer = _repository.GetByID(offerId); // Use GetByID to fetch the offer
            if (offer == null)
                return false;

            foreach (var roomId in roomIds)
            {
                // Check if the room is already assigned
                if (offer.OfferRooms.All(or => or.RoomId != roomId))
                {
                    offer.OfferRooms.Add(new OfferRoom
                    {
                        RoomId = roomId,
                        offer = offer
                    });
                }
            }

            // Update the offer with the new room assignments
            _repository.Update(offer);

            // Save changes synchronously if your repository doesn't support async save
            _repository.SaveChanges();

            return true;
        }

        public async Task<Offer> UpdateAsync(int id, EditOfferDto editOfferDTO)
        {
            var offerFromDb = _repository.GetByID(id);
            if (offerFromDb == null)
            {
                return null;
            }

            // Map the updated data excluding OfferRooms
            var offer = editOfferDTO.MapOne<Offer>();
            offer.Id = id;

            _repository.Update(offer);
            _repository.SaveChanges();

            return offer;
        }

        public async Task<bool> UpdateAssignedRoomsToOfferAsync(int offerId, IEnumerable<int> roomIds)
        {
            var offer = _repository.GetByID(offerId);
            if (offer == null)
            {
                return false;
            }

            // Clear existing OfferRooms
            offer.OfferRooms.Clear();

            // Handle the OfferRooms relationships based on RoomIds in the DTO
            foreach (var roomId in roomIds)
            {
                offer.OfferRooms.Add(new OfferRoom
                {
                    RoomId = roomId,
                    offer = offer
                });
            }

            // Update the offer with the new room assignments
            _repository.Update(offer);
            _repository.SaveChanges();

            return true;
        }

        public void Delete(int id)
        {
            var offer = _repository.GetByID(id);

            if (offer != null)
            {
                _repository.Delete(id);
                _repository.SaveChanges();
            }
        }

        public void DeleteAssignedRooms(int offerId)
        {
            var offer = _repository.GetByID(offerId);

            if (offer != null)
            {
                // Remove all related OfferRooms
                foreach (var offerRoom in offer.OfferRooms.ToList())
                {
                    _repository.Delete(offerRoom.Id);
                }

                offer.OfferRooms.Clear();
                _repository.Update(offer);
                _repository.SaveChanges();
            }
        }


    }

}

