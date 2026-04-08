using Application.DTOs;
using MediatR;

namespace Application.Jobs.Queries
{
    public class GetJobByIdQuery : IRequest<JobResponseDto?>
    {
        public int Id { get; set; }
    }
}