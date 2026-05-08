using AutoMapper;
using RetailSystem.Application.Dtos.Categories;
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

        public List<Category> GetAllCategories()
        {
            return _unitOfWork.Categories.GetAll().ToList();
        }

        public async Task<List<CategoryDto>> AddCategoryAsync(List<CreateCategoryCommand> command)
        {
            var categories = _mapper.Map<List<Category>>(command);

            await _unitOfWork.Categories.AddRangeAsync(categories);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryCommand command)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(command.Id);

            if (category == null)
            {
                throw new Exception("Category not found");
            }

            category.CategoryName = command.CategoryName;
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
