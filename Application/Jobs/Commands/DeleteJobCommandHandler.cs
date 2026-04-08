using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Commands
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteJobCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j => j.Id == request.Id, cancellationToken);

            if (job == null)
                throw new Exception($"Job with ID {request.Id} not found");

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}