using Application.Jobs.Commands;
using Application.Jobs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ISender _mediator;

        public JobController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllJobsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetJobByIdQuery { Id = id });
            if (result == null)
                return NotFound(new { message = $"Job with ID {id} not found" });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { message = "Job created successfully", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateJobCommand command)
        {
            command.Id = id;
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Job updated successfully" });
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
                await _mediator.Send(new DeleteJobCommand { Id = id });
                return Ok(new { message = "Job deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}