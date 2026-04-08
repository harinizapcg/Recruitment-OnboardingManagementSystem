using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Jobs.Commands
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateJobCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = new Job
            {
                RequisitionId = request.RequisitionId,
                Title = request.Title,
                Description = request.Description,
                RequiredSkills = request.RequiredSkills,
                ExperienceRequired = request.ExperienceRequired,
                Location = request.Location,
                Status = request.Status,
                CreatedAt = DateTime.UtcNow
            };

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync(cancellationToken);

            return job.Id;
        }
    }
}