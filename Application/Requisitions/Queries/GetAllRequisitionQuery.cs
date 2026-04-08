using Application.DTOs;
using MediatR;

namespace Application.Requisitions.Queries
{
    public class GetAllRequisitionsQuery : IRequest<List<RequisitionResponseDto>>
    {
    }
}