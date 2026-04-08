using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requisitions.Commands
{
    public class UpdateRequisitionCommandHandler : IRequestHandler<UpdateRequisitionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateRequisitionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateRequisitionCommand request, CancellationToken cancellationToken)
        {
            var requisition = await _context.Requisitions
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (requisition == null)
                throw new Exception($"Requisition with ID {request.Id} not found");

            if (request.Title != null) requisition.Title = request.Title;
            if (request.Description != null) requisition.Description = request.Description;
            if (request.RequiredSkills != null) requisition.RequiredSkills = request.RequiredSkills;
            if (request.ExperienceRequired.HasValue) requisition.ExperienceRequired = request.ExperienceRequired.Value;
            if (request.Priority != null) requisition.Priority = request.Priority;
            if (request.Status != null) requisition.Status = request.Status;
            requisition.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}