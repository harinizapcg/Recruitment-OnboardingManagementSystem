using Application.DTOs;
using MediatR;

namespace Application.Candidates.Queries
{
    public class GetAllCandidatesQuery : IRequest<List<CandidateResponseDto>>
    {
    }
}