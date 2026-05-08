using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Domain.Entities;
using RetailSystem.Infrastructure.Data;

namespace RetailSystem.Infrastructure.Repositories
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        public ColorRepository(RetailSystemDbContext context) : base(context)
        {
            
        }
    }
}
