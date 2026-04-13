using MediatR;

public class GetOfferByApplicationIdHandler
    : IRequestHandler<GetOfferByApplicationIdQuery, Offer>
{
    private readonly IOfferRepository _repository;

    public GetOfferByApplicationIdHandler(IOfferRepository repository)
    {
        _repository = repository;
    }

    public async Task<Offer> Handle(
        GetOfferByApplicationIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetByApplicationIdAsync(request.ApplicationId);
    }
}