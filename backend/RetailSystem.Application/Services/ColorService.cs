using RetailSystem.Application.Dtos.Colors;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Application.Interfaces.UnitOfWork;

namespace RetailSystem.Application.Services
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ColorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ColorDto>> GetAllColorsAsync()
        {
            var colors = await _unitOfWork.Colors.GetAllAsync();
            return colors.Select(c => new ColorDto
            {
                Id = c.Id,
                ColorName = c.ColorName,
            }).OrderBy(c => c.ColorName).ToList();
        }
    }
}
