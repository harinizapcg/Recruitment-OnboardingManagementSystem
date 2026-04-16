using MediatR;
using Microsoft.AspNetCore.Http;

public class ApplyToJobCommand : IRequest<int>
{
    public int JobId { get; set; }
    public int CandidateId { get; set; }

    public IFormFile Resume { get; set; }
    public IFormFile? CoverLetter { get; set; }
}