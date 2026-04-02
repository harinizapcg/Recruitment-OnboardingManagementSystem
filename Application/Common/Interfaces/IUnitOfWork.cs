namespace Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Add your repositories here
    // Example: IRepository<YourEntity> YourEntities { get; }
    
    Task<int> SaveChangesAsync();
}
