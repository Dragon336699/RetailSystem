using RetailSystem.Application.Interfaces.Repositories;

namespace RetailSystem.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
