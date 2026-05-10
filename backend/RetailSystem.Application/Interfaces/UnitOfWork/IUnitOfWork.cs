using RetailSystem.Application.Interfaces.Repositories;

namespace RetailSystem.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IProductImageRepository ProductImages { get; }
        IProductVariantRepository ProductVariants { get; }
        ISizeRepository Sizes { get;  }
        int Complete();
        Task<int> CompleteAsync();
    }
}
