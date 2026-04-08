using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Requisitions.Commands
{
    public class CreateRequisitionCommandHandler : IRequestHandler<CreateRequisitionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateRequisitionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRequisitionCommand request, CancellationToken cancellationToken)
        {
            var requisition = new Requisition
            {
                Title = request.Title,
                Description = request.Description,
                RequiredSkills = request.RequiredSkills,
                ExperienceRequired = request.ExperienceRequired,
                Priority = request.Priority,
                Status = request.Status,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow
            };

            _context.Requisitions.Add(requisition);
            await _context.SaveChangesAsync(cancellationToken);

            return requisition.Id;
        }
    }
}