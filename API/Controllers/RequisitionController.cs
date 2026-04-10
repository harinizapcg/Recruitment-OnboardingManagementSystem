using Application.Requisitions.Commands;
using Application.Requisitions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RequisitionController : ControllerBase
    {
        private readonly ISender _mediator;

        public RequisitionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllRequisitionsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetRequisitionByIdQuery { Id = id });
            if (result == null)
                return NotFound(new { message = $"Requisition with ID {id} not found" });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequisitionCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { message = "Requisition created successfully", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateRequisitionCommand command)
        {
            command.Id = id;
            try
            {
                await _mediator.Send(command);
                return Ok(new { message = "Requisition updated successfully" });
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
                await _mediator.Send(new DeleteRequisitionCommand { Id = id });
                return Ok(new { message = "Requisition deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}