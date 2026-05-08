using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Domain.Entities;
using RetailSystem.Infrastructure.Data;

namespace RetailSystem.Infrastructure.Repositories
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        public SizeRepository(RetailSystemDbContext context) : base(context)
        {
            
        }
    }
}
