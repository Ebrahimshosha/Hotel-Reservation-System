
namespace Hotel_Reservation_System.Controllers;

public class FacilitiesController : BaseApiController
{
    private readonly IFacilityMediator _mediator;
    //ahmed modify
    public FacilitiesController(IFacilityMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public IActionResult GetAllFaility()
    {
        var facilities = _mediator.getAllFacilities();
        return Ok(facilities);
    }

    [HttpPost("")]
    public IActionResult Addfacility(Facility facility)
    {
        facility = _mediator.Add(facility);
        return Ok(facility);
    }

    [HttpPut("{id}")]
    public IActionResult Updatefacility(int id, Facility facility)
    {
        facility = _mediator.Update(id, facility);
        return Ok(facility);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletefacility(int id) 
    {
        _mediator.DeleteFacility(id);
        return Ok();
    }
}

