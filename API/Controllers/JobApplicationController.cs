using Application.JobApplications.Commands;
using Application.JobApplications.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JobApplicationController : ControllerBase
{
    private readonly ISender _mediator;

    public JobApplicationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("apply")]
    public async Task<IActionResult> Apply([FromForm] ApplyToJobCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("job/{jobId}")]
    public async Task<IActionResult> GetByJob(int jobId)
    {
        var result = await _mediator.Send(new GetApplicationsByJobQuery { JobId = jobId });
        return Ok(result);
    }

    [HttpPut("shortlist/{id}")]
    public async Task<IActionResult> Shortlist(int id)
    {
        await _mediator.Send(new ShortlistCandidateCommand
        {
            ApplicationId = id,
            Comments = null
        });
        return Ok(new { message = "Candidate shortlisted" });
    }

    [HttpPut("reject/{id}")]
    public async Task<IActionResult> Reject(int id)
    {
        await _mediator.Send(new RejectCandidateCommand
        {
            ApplicationId = id,
            Comments = null
        });
        return Ok(new { message = "Candidate rejected" });
    }
}