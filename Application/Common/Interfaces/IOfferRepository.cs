public interface IOfferRepository
{
    Task<int> CreateAsync(Offer offer);
    Task<Offer> GetByApplicationIdAsync(int applicationId);
}