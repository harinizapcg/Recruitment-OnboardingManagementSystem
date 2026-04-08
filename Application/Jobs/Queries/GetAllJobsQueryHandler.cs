using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Queries
{
    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, List<JobResponseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllJobsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobResponseDto>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Jobs
                .Include(j => j.Requisition)
                .Select(j => new JobResponseDto
                {
                    Id = j.Id,
                    RequisitionId = j.RequisitionId,
                    RequisitionTitle = j.Requisition.Title,
                    Title = j.Title,
                    Description = j.Description,
                    RequiredSkills = j.RequiredSkills,
                    ExperienceRequired = j.ExperienceRequired,
                    Location = j.Location,
                    Status = j.Status,
                    CreatedAt = j.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}