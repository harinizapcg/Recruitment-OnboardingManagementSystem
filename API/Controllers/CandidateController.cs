using Application.Candidates.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Candidates.Queries;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ISender _mediator;

        public CandidateController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCandidatesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCandidateByIdQuery { Id = id });
            if (result == null)
                return NotFound(new { message = $"Candidate with ID {id} not found" });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCandidateCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { message = "Candidate created successfully", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCandidateCommand command)
        {
            command.Id = id;
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Candidate updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new DeleteCandidateCommand { Id = id });
                return Ok(new { message = "Candidate deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}