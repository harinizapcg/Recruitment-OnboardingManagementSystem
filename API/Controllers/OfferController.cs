using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OfferController : ControllerBase
{
    private readonly ISender _mediator;

    public OfferController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate(GenerateOfferCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { message = "Offer generated", id });
    }

    [HttpGet("{applicationId}")]
    public async Task<IActionResult> Get(int applicationId)
    {
        var result = await _mediator.Send(
            new GetOfferByApplicationIdQuery { ApplicationId = applicationId });

        return Ok(result);
    }
}