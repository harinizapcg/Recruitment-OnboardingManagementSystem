using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requisitions.Commands
{
    public class DeleteRequisitionCommandHandler : IRequestHandler<DeleteRequisitionCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteRequisitionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteRequisitionCommand request, CancellationToken cancellationToken)
        {
            var requisition = await _context.Requisitions
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (requisition == null)
                throw new Exception($"Requisition with ID {request.Id} not found");

            _context.Requisitions.Remove(requisition);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}