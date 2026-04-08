using Application.Common.Interfaces;
using MediatR;

namespace Application.JobApplications.Commands;

public class ApplyToJobCommandHandler : IRequestHandler<ApplyToJobCommand, int>
{
    private readonly IApplicationDbContext _context;

    public ApplyToJobCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(ApplyToJobCommand request, CancellationToken cancellationToken)
    {
        var application = new JobApplication
        {
            JobId = request.JobId,
            CandidateId = request.CandidateId,
            Status = "Applied"
        };

        _context.JobApplications.Add(application);
        await _context.SaveChangesAsync(cancellationToken);

        return application.Id;
    }
}