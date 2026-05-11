using RetailSystem.SharedLibrary.Dtos.Sizes;

namespace RetailSystem.UI.Services.Intefaces
{
    public interface ISizeApiClient
    {
        Task<List<SizeDto>> GetSizesAsync();
    }
}
