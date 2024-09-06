using Hotel_Reservation_System.Consts;
using Hotel_Reservation_System.DTO.Authorization;
using Hotel_Reservation_System.Models;
using Hotel_Reservation_System.Services.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation_System.Controllers;

[Authorize(Roles = DefaultRoles.Admin)]
public class RolesController(IRoleService roleService) : BaseApiController
{
    private readonly IRoleService _roleService = roleService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled, CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllAsync(includeDisabled, cancellationToken);

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var result = await _roleService.GetAsync(id);

        return result.IsSuccess ? Ok(result.Value) : BadRequest();
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] RoleRequest request)
    {
        var result = await _roleService.AddAsync(request);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] RoleRequest request)
    {
        var result = await _roleService.UpdateAsync(id, request);

        return result.IsSuccess ? NoContent() : BadRequest();
    }

    [HttpPut("{id}/toggle-status")]
    public async Task<IActionResult> ToggleStatus([FromRoute] string id)
    {
        var result = await _roleService.ToggleStatusAsync(id);

        return result.IsSuccess ? NoContent() : BadRequest();
    }
}
