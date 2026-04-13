using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OnboardingController : ControllerBase
{
    private readonly ISender _mediator;

    public OnboardingController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("upload-docs")]
    public async Task<IActionResult> Upload(UploadDocumentsCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { message = "Documents uploaded", id });
    }

    [HttpPut("verify/{applicationId}")]
    public async Task<IActionResult> Verify(int applicationId)
    {
        var result = await _mediator.Send(
            new VerifyOnboardingCommand { ApplicationId = applicationId });

        if (!result)
            return NotFound();

        return Ok(new { message = "Candidate onboarded successfully" });
    }

    [HttpGet("{applicationId}")]
    public async Task<IActionResult> Get(int applicationId)
    {
        var result = await _mediator.Send(
            new GetOnboardingByApplicationIdQuery { ApplicationId = applicationId });

        return Ok(result);
    }
}