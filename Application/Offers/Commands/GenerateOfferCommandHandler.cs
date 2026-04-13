using MediatR;

public class GenerateOfferCommandHandler
    : IRequestHandler<GenerateOfferCommand, int>
{
    private readonly IOfferRepository _repository;

    public GenerateOfferCommandHandler(IOfferRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(
        GenerateOfferCommand request,
        CancellationToken cancellationToken)
    {
        var offer = new Offer
        {
            ApplicationId = request.ApplicationId,
            Salary = request.Salary,
            JoiningDate = request.JoiningDate
        };

        return await _repository.CreateAsync(offer);
    }
}