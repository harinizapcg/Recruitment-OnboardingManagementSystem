using Application.Interviews.Commands;
using Application.Interviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InterviewController : ControllerBase
{
    private readonly ISender _mediator;

    public InterviewController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("schedule")]
    public async Task<IActionResult> Schedule(ScheduleInterviewCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { message = "Interview scheduled", id });
    }

    [HttpGet("application/{applicationId}")]
    public async Task<IActionResult> GetByApplication(int applicationId)
    {
        var result = await _mediator.Send(
            new GetInterviewsByApplicationQuery { JobApplicationId = applicationId });
        return Ok(result);
    }

    [HttpPost("feedback")]
    public async Task<IActionResult> SubmitFeedback(SubmitFeedbackCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { message = "Feedback submitted", id });
    }

    [HttpGet("feedback/{applicationId}")]
    public async Task<IActionResult> GetFeedback(int applicationId)
    {
        var result = await _mediator.Send(
            new GetFeedbackByApplicationIdQuery { ApplicationId = applicationId });
        return Ok(result);
    }
}