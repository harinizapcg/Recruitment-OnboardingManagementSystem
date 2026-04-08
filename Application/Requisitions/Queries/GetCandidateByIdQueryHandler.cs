using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requisitions.Queries
{
    public class GetRequisitionByIdQueryHandler : IRequestHandler<GetRequisitionByIdQuery, RequisitionResponseDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetRequisitionByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RequisitionResponseDto?> Handle(GetRequisitionByIdQuery request, CancellationToken cancellationToken)
        {
            var requisition = await _context.Requisitions
                .Include(r => r.CreatedByUser)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (requisition == null) return null;

            return new RequisitionResponseDto
            {
                Id = requisition.Id,
                Title = requisition.Title,
                Description = requisition.Description,
                RequiredSkills = requisition.RequiredSkills,
                ExperienceRequired = requisition.ExperienceRequired,
                Priority = requisition.Priority,
                Status = requisition.Status,
                CreatedBy = requisition.CreatedBy,
                CreatedByName = requisition.CreatedByUser?.Name ?? "",
                CreatedAt = requisition.CreatedAt
            };
        }
    }
}