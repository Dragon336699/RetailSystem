using RetailSystem.Application.Dtos.Colors;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface IColorService
    {
        Task<List<ColorDto>> GetAllColorsAsync();
    }
}
