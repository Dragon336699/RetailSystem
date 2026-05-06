using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Domain.Entities;
using RetailSystem.Infrastructure.Data;

namespace RetailSystem.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(RetailSystemDbContext context) : base(context)
        {
        }
    }
}
