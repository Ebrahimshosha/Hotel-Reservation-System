
using Hotel_Reservation_System.DTO.Facility;
using Hotel_Reservation_System.ViewModels.FacilitiesViewModel;

namespace Hotel_Reservation_System.Controllers;

public class FacilitiesController : BaseApiController
{
    private readonly IFacilityMediator _mediator;

    public FacilitiesController(IFacilityMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public IActionResult GetAllFaility()
    {//ahmed
        var facilitiesToReturnDto = _mediator.getAllFacilities();
        return Ok(facilitiesToReturnDto);
    }
    [HttpGet("{id}")]
    public FacilityToReturnDto GetFacilityById([FromRoute] int id)
    {
        var facilityToReturnDto = _mediator.GetById(id);
        return facilityToReturnDto;
    }

    [HttpPost("")]
    public IActionResult Addfacility([FromBody] CreateFacilityViewModel viewModel)
    {
        var facilityToReturnDto = _mediator.Add(viewModel);

        return Ok(facilityToReturnDto);
    }

    [HttpPut("{id}")]
    public IActionResult Updatefacility([FromRoute] int id, [FromBody] CreateFacilityViewModel viewModel)
    {
        var facilityToReturnDto = _mediator.Update(id, viewModel);
        return Ok(facilityToReturnDto);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletefacility([FromRoute] int id)
    {
        var isDeleted = _mediator.DeleteFacility(id);

        if (isDeleted)
        {

            return Ok(true);
        }
        return Ok(false);
    }
}

