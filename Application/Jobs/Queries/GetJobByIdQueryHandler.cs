using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Queries
{
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobResponseDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetJobByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobResponseDto?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .Include(j => j.Requisition)
                .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);

            if (job == null) return null;

            return new JobResponseDto
            {
                Id = job.Id,
                RequisitionId = job.RequisitionId,
                RequisitionTitle = job.Requisition.Title,
                Title = job.Title,
                Description = job.Description,
                RequiredSkills = job.RequiredSkills,
                ExperienceRequired = job.ExperienceRequired,
                Location = job.Location,
                Status = job.Status,
                CreatedAt = job.CreatedAt
            };
        }
    }
}