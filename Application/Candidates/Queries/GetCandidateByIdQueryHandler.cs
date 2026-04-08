using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Candidates.Queries
{
    public class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, CandidateResponseDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetCandidateByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CandidateResponseDto?> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (candidate == null) return null;

            return new CandidateResponseDto
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Email = candidate.Email,
                Phone = candidate.Phone,
                Skills = candidate.Skills,
                Experience = candidate.Experience,
                ResumePath = candidate.ResumePath,
                Source = candidate.Source,
                CreatedAt = candidate.CreatedAt
            };
        }
    }
}