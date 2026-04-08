using Application.DTOs;
using MediatR;

namespace Application.Jobs.Queries
{
    public class GetAllJobsQuery : IRequest<List<JobResponseDto>>
    {
    }
}