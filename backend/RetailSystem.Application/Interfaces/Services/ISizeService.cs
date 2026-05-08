using RetailSystem.Application.Dtos.Sizes;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface ISizeService
    {
        Task<List<SizeDto>> GetAllSizesAsync();
    }
}
