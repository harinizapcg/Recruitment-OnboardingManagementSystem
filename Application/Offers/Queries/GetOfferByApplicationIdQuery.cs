using MediatR;

public class GetOfferByApplicationIdQuery : IRequest<Offer>
{
    public int ApplicationId { get; set; }
}