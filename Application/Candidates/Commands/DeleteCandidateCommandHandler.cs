using Application.Candidates.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Candidates.Handlers
{
    public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand>
    {
        private readonly ICandidateRepository _repository;

        public DeleteCandidateCommandHandler(ICandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsAsync(request.Id);
            if (!exists)
                throw new Exception($"Candidate with ID {request.Id} not found.");

            await _repository.DeleteAsync(request.Id);
        }
    }
}