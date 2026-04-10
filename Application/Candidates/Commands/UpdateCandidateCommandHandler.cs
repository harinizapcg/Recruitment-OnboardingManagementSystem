using Application.Candidates.Commands;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Candidates.Handlers
{
    public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, bool>
    {
        private readonly ICandidateRepository _candidateRepository;

        public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.Id);
            if (candidate is null)
                throw new Exception($"Candidate with ID {request.Id} not found.");

            candidate.Name = request.Name;
            candidate.Email = request.Email;
            candidate.Phone = request.Phone;
            candidate.Skills = request.Skills;
            candidate.Experience = request.Experience;
            candidate.ResumePath = request.ResumePath;
            candidate.Source = request.Source;

            await _candidateRepository.UpdateAsync(candidate);
            return true;
        }
    }
}