using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class OfferRepository : IOfferRepository
{
    private readonly ApplicationDbContext _context;

    public OfferRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(Offer offer)
    {
        _context.Offers.Add(offer);
        await _context.SaveChangesAsync();
        return offer.OfferId;
    }

    public async Task<Offer> GetByApplicationIdAsync(int applicationId)
    {
        return await _context.Offers
            .FirstOrDefaultAsync(o => o.ApplicationId == applicationId);
    }
}