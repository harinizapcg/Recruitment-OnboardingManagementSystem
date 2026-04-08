using Application.DTOs;
using MediatR;

namespace Application.Candidates.Queries
{
    public class GetCandidateByIdQuery : IRequest<CandidateResponseDto?>
    {
        public int Id { get; set; }
    }
}