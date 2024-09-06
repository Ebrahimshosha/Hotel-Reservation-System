using Hotel_Reservation_System.DTO.Offer;
using Hotel_Reservation_System.Exceptions.Error;
using Hotel_Reservation_System.Mediators.OfferMediator;
using Hotel_Reservation_System.ViewModels.Offer;
using Hotel_Reservation_System.ViewModels.ResultViewModel;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Controllers
{
    public class OffersController : BaseApiController
    {
        private readonly IOfferMediator _offerMediator;

        public OffersController(IOfferMediator offerMediator)
        {
            _offerMediator = offerMediator;
        }

        [HttpPost("Add")]
        public async Task<ResultViewModel<OfferViewModel>> AddOffer([FromForm] CreateOfferViewModel viewModel)
        {
            var createOfferDto = viewModel.MapOne<AddOfferDto>();
            var offerToReturnDto = await _offerMediator.Add(createOfferDto);
            var offerViewModel = offerToReturnDto.MapOne<OfferViewModel>();
            return ResultViewModel<OfferViewModel>.Sucess(offerViewModel);
        }




        [HttpPost("AssignRooms/{offerId}")]
        public async Task<ResultViewModel<bool>> AssignRoomsToOffer(int offerId, [FromBody] IEnumerable<int> roomIds)
        {
            var success = await _offerMediator.AssignRoomsToOfferAsync(offerId, roomIds);
            if (success)
            {
                return ResultViewModel<bool>.Sucess(true);
            }
            return ResultViewModel<bool>.Faliure(ErrorCode.ResourceNotFound, "Failed to assign rooms to offer.");
        }
        [HttpPut("{id}/AssignRooms")]
        public async Task<ResultViewModel<bool>> UpdateAssignedRoomsToOffer([FromRoute] int id, [FromBody] IEnumerable<int> roomIds)
        {
            var success = await _offerMediator.UpdateAssignedRoomsToOfferAsync(id, roomIds);
            if (success)
            {
                return ResultViewModel<bool>.Sucess(true);
            }
            return ResultViewModel<bool>.Faliure(ErrorCode.ResourceNotFound, "Failed to update assigned rooms for offer.");
        }

        [HttpPut("{id}")]
        public async Task<ResultViewModel<OfferViewModel>> UpdateOffer([FromRoute] int id, [FromForm] CreateOfferViewModel viewModel)
        {
            var editOfferDto = viewModel.MapOne<EditOfferDto>();
            var offerToReturnDTO = await _offerMediator.Update(id, editOfferDto);
            var offerViewModel = offerToReturnDTO.MapOne<OfferViewModel>();

            return ResultViewModel<OfferViewModel>.Sucess(offerViewModel);
        }


        [HttpDelete("{id}")]
        public ResultViewModel<bool> DeleteOffer([FromRoute] int id)
        {
            var isDeleted = _offerMediator.Delete(id);

            if (isDeleted)
            {
                return ResultViewModel<bool>.Sucess(true, "Offer is Deleted");
            }

            return ResultViewModel<bool>.Faliure(ErrorCode.ResourceNotFound, "Offer does not exist");
        }

        [HttpDelete("{id}/DeleteRooms")]
        public ResultViewModel<bool> DeleteAssignedRooms([FromRoute] int id)
        {
            var isDeleted = _offerMediator.DeleteAssignedRooms(id);

            if (isDeleted)
            {
                return ResultViewModel<bool>.Sucess(true, "Assigned rooms are Deleted");
            }

            return ResultViewModel<bool>.Faliure(ErrorCode.ResourceNotFound, "Offer does not exist or has no assigned rooms");
        }
    }
}