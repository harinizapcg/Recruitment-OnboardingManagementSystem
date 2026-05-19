using Application.Roles.Commands;
using Application.Roles.DTOs;
using Application.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [AllowAnonymous]  // ✅ Allow unauthenticated access for Register/Login dropdowns
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery { RoleId = id });
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateRoleCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] UpdateRoleCommand command)
        {
            if (id != command.RoleId) return BadRequest("Route ID and body ID must match.");
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand { RoleId = id });
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}