using FluentValidation;
using RetailSystem.Application.Dtos.Products;

namespace RetailSystem.Application.Validation.Products
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(p => p.CategoryIds)
                .NotEmpty().WithMessage("At least one category is required.");
            RuleFor(p => p.ProductImages)
                .NotEmpty().WithMessage("At least one product image is required.");
            RuleFor(p => p.SizesQuantity)
                .NotEmpty().WithMessage("At least one size and quantity is required.");
        }
    }
}
