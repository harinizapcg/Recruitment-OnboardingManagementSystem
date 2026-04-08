using Application.DTOs;
using MediatR;

namespace Application.Requisitions.Queries
{
    public class GetRequisitionByIdQuery : IRequest<RequisitionResponseDto?>
    {
        public int Id { get; set; }
    }
}