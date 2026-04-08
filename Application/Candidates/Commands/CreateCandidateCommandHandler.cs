using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Candidates.Commands
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCandidateCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _context.Candidates
                .AnyAsync(c => c.Email == request.Email, cancellationToken);

            if (emailExists)
                throw new Exception("Candidate with this email already exists");

            var candidate = new Candidate
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Skills = request.Skills,
                Experience = request.Experience,
                ResumePath = request.ResumePath,
                Source = request.Source,
                CreatedAt = DateTime.UtcNow
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync(cancellationToken);

            return candidate.Id;
        }
    }
}