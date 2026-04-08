using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Commands
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateJobCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);

            if (job == null)
                throw new Exception($"Job with ID {request.Id} not found");

            if (request.Title != null) job.Title = request.Title;
            if (request.Description != null) job.Description = request.Description;
            if (request.RequiredSkills != null) job.RequiredSkills = request.RequiredSkills;
            if (request.ExperienceRequired.HasValue) job.ExperienceRequired = request.ExperienceRequired.Value;
            if (request.Location != null) job.Location = request.Location;
            if (request.Status != null) job.Status = request.Status;
            job.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}