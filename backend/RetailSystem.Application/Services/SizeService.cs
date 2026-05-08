using RetailSystem.Application.Dtos.Sizes;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Application.Interfaces.UnitOfWork;

namespace RetailSystem.Application.Services
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SizeDto>> GetAllSizesAsync()
        {
            var sizes = await _unitOfWork.Sizes.GetAllAsync();
            return sizes.Select(s => new SizeDto
            {
                Id = s.Id,
                SizeNumber = s.SizeNumber
            }).OrderBy(s => s.SizeNumber).ToList();
        }
    }
}
