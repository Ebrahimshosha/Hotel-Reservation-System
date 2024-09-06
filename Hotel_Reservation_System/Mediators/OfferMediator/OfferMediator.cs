using Hotel_Reservation_System.DTO.Offer;

namespace Hotel_Reservation_System.Mediators.OfferMediator
{
    public class OfferMediator : IOfferMediator
    {
        private readonly StoreContext _context;

        public OfferMediator(StoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Offer> GetAll()
        {
            return _context.Offers.ToList();
        }

        public Offer Get(int id)
        {
            return _context.Offers.Find(id);
        }

        public async Task<OfferDTO> Add(AddOfferDto addOfferDTO)
        {
            var offer = addOfferDTO.MapOne<Offer>();

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return offer.MapOne<OfferDTO>();
        }

        public async Task<bool> AssignRoomsToOfferAsync(int offerId, IEnumerable<int> roomIds)
        {
            var offer = await _context.Offers
                .Include(o => o.OfferRooms)
                .FirstOrDefaultAsync(o => o.Id == offerId);

            if (offer == null)
                return false;

            foreach (var roomId in roomIds)
            {
                if (offer.OfferRooms.All(or => or.RoomId != roomId))
                {
                    offer.OfferRooms.Add(new OfferRoom
                    {
                        RoomId = roomId,
                        offer = offer
                    });
                }
            }

            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<OfferDTO> Update(int id, EditOfferDto editOfferDTO)
        {
            var offer = await _context.Offers
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
                return null;

            // Map the updated data excluding OfferRooms
            var updatedOffer = editOfferDTO.MapOne<Offer>();
            updatedOffer.Id = id;

            _context.Offers.Update(updatedOffer);
            await _context.SaveChangesAsync();

            return updatedOffer.MapOne<OfferDTO>();
        }

        public async Task<bool> UpdateAssignedRoomsToOfferAsync(int offerId, IEnumerable<int> roomIds)
        {
            var offer = await _context.Offers
                .Include(o => o.OfferRooms)
                .FirstOrDefaultAsync(o => o.Id == offerId);

            if (offer == null)
                return false;

            // Clear existing OfferRooms
            _context.offerRooms.RemoveRange(offer.OfferRooms);
            offer.OfferRooms.Clear();

            // Add new Data
            foreach (var roomId in roomIds)
            {
                offer.OfferRooms.Add(new OfferRoom
                {
                    RoomId = roomId,
                    offer = offer
                });
            }

            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();

            return true;
        }


        public bool Delete(int id)
        {
            // Load the offer along with its related offer rooms
            var offer = _context.Offers
                .Include(o => o.OfferRooms)
                .FirstOrDefault(o => o.Id == id);

            if (offer == null)
                return false;

            // Soft delete the offer
            offer.IsDeleted = true;

            // Update the offer in the database and save changes
            _context.Offers.Update(offer);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteAssignedRooms(int offerId)
        {
            var offer = _context.Offers
                .Include(o => o.OfferRooms)
                .FirstOrDefault(o => o.Id == offerId);

            if (offer == null)
                return false;

            // Remove all related OfferRooms
            _context.offerRooms.RemoveRange(offer.OfferRooms);

            // Clear the OfferRooms collection
            offer.OfferRooms.Clear();
            _context.Offers.Update(offer);
            _context.SaveChanges();

            return true;
        }


    }

}
