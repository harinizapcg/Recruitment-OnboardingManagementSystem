using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requisitions.Queries
{
    public class GetAllRequisitionsQueryHandler : IRequestHandler<GetAllRequisitionsQuery, List<RequisitionResponseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllRequisitionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RequisitionResponseDto>> Handle(GetAllRequisitionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Requisitions
                .Include(r => r.CreatedByUser)
                .Select(r => new RequisitionResponseDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    RequiredSkills = r.RequiredSkills,
                    ExperienceRequired = r.ExperienceRequired,
                    Priority = r.Priority,
                    Status = r.Status,
                    CreatedBy = r.CreatedBy,
                    CreatedByName = r.CreatedByUser != null ? r.CreatedByUser.Name : "",
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}