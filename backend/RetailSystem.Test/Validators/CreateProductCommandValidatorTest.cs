using FluentValidation.TestHelper;
using RetailSystem.Application.Validation.Products;
using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.SharedLibrary.Dtos.ImagesUpload;
using RetailSystem.SharedLibrary.Dtos.Products;

namespace RetailSystem.Test.Validators
{
    public class CreateProductCommandValidatorTest
    {
        [Fact]
        public void Should_Not_Have_Error_When_Input_Is_Valid()
        {
            // Arrange
            var validator = new CreateProductCommandValidator();

            var model = CreateValidModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Is_Empty()
        {
            var validator = new CreateProductCommandValidator();
            var model = CreateValidModel() with { ProductName = ""};

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Too_Long()
        {
            var validator = new CreateProductCommandValidator();
            var model = CreateValidModel() with { ProductName = new string('A', 101) };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        //PRICE

        [Fact]
        public void Should_Have_Error_When_Price_Is_Zero()
        {
            var validator = new CreateProductCommandValidator();
            var model = CreateValidModel() with { Price = 0 };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Negative()
        {
            var validator = new CreateProductCommandValidator();
            var model = CreateValidModel() with { Price = -10 };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        // CATEGORYIDS

        [Fact]
        public void Should_Have_Error_When_CategoryIds_Is_Empty()
        {
            var validator = new CreateProductCommandValidator();
            var model = CreateValidModel() with { CategoryIds = new List<Guid>()};

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.CategoryIds);
        }

        //PRODUCT IMAGES

        [Fact]
        public void Should_Have_Error_When_ProductImages_Is_Empty()
        {
            var validator = new CreateProductCommandValidator();
            var model = CreateValidModel() with { ProductImages = new List<ImageUploadDto>() };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ProductImages);
        }

        //SIZE
        [Fact]
        public void Should_Have_Error_When_SizesQuantity_Is_Empty()
        {
            var validator = new CreateProductCommandValidator();
            var model = CreateValidModel() with { SizesQuantity = new List<UploadProductSizeDto>() };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.SizesQuantity);
        }
        private CreateProductCommand CreateValidModel()
        {
            var model = new CreateProductCommand
            {
                ProductName = "Product A",
                Price = 120.99m,
                Description = "Description",
                ThumbnailIndex = 0,

                CategoryIds = new List<Guid>
        {
            Guid.NewGuid()
        },

                ProductImages = new List<ImageUploadDto>
        {
            new ImageUploadDto
            {
                Content = new MemoryStream(new byte[] { 1, 2, 3 }),
                FileName = "image1.jpg",
                ContentType = "image/jpeg"
            }
        },

                SizesQuantity = new List<UploadProductSizeDto>
        {
            new UploadProductSizeDto
            {
                SizeId = Guid.NewGuid(),
                Quantity = 10
            }
        }
            };

            return model;
        }
    }
}
