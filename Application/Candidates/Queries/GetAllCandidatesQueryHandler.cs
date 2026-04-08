using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Candidates.Queries
{
    public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, List<CandidateResponseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCandidatesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CandidateResponseDto>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Candidates
                .Select(c => new CandidateResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    Skills = c.Skills,
                    Experience = c.Experience,
                    ResumePath = c.ResumePath,
                    Source = c.Source,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}