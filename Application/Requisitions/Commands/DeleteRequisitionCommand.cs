using MediatR;

namespace Application.Requisitions.Commands
{
    public class DeleteRequisitionCommand : IRequest
    {
        public int Id { get; set; }
    }
}