using RetailSystem.SharedLibrary.Dtos.Sizes;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface ISizeService
    {
        Task<List<SizeDto>> GetAllSizesAsync();
    }
}
