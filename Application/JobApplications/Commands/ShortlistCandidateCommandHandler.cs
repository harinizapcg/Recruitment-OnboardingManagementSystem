using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.JobApplications.Commands;

public class ShortlistCandidateCommandHandler
    : IRequestHandler<ShortlistCandidateCommand>
{
    private readonly IApplicationDbContext _context;

    public ShortlistCandidateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ShortlistCandidateCommand request, CancellationToken cancellationToken)
    {
        var application = await _context.JobApplications
            .FirstOrDefaultAsync(x => x.Id == request.ApplicationId, cancellationToken);

        if (application == null)
            throw new Exception("Application not found");

        application.Status = "Shortlisted";
        application.ScreeningComments = request.Comments;

        await _context.SaveChangesAsync(cancellationToken);
    }
}