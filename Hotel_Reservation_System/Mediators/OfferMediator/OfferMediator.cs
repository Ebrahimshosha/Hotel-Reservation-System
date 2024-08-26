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
            // Add OfferRooms based on RoomIds
            foreach (var roomId in addOfferDTO.RoomIds)
            {
                offer.OfferRooms.Add(new OfferRoom
                {
                    RoomId = roomId,
                    offer = offer
                });
            }

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return offer.MapOne<OfferDTO>();
        }

        public async Task<OfferDTO> Update(int id, EditOfferDto editOfferDTO)
        {
            var offer = await _context.Offers
                .Include(o => o.OfferRooms)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
                return null;

            // Remove old Data
            _context.offerRooms.RemoveRange(offer.OfferRooms);

            // Clear the memory 
            offer.OfferRooms.Clear();

            // Add new Data
            foreach (var roomId in editOfferDTO.RoomIds)
            {
                offer.OfferRooms.Add(new OfferRoom
                {
                    RoomId = roomId,
                    offer = offer
                });
            }

            // Update the offer in the database
            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();
            return offer.MapOne<OfferDTO>();
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

            // Soft delete related offer rooms
            foreach (var offerRoom in offer.OfferRooms)
            {
                offerRoom.IsDeleted = true;
            }

            // Update the offer in the database and save changes
            _context.Offers.Update(offer);
            _context.SaveChanges();

            return true;
        }


    }

}
