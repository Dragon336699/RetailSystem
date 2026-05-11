using AutoMapper;
using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Domain.Entities;

namespace RetailSystem.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            IEnumerable<Category> category = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(category);
        }

        public async Task<CategoryDto> AddCategoryAsync(CreateCategoryCommand command)
        {
            var category = _mapper.Map<Category>(command);

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryCommand command)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(command.Id);

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            category.CategoryName = command.CategoryName;
            category.Description = command.Description;
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.CompleteAsync();
        }
    }
}
