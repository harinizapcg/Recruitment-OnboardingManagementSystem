using Application.Users.Commands;
using Application.Users.DTOs;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { UserId = id });
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Update(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.UserId) return BadRequest("Route ID and body ID must match.");
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { UserId = id });
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}