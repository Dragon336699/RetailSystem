using FluentValidation.TestHelper;
using RetailSystem.Application.Validation.Products;
using RetailSystem.SharedLibrary.Dtos.ImagesUpload;
using RetailSystem.SharedLibrary.Dtos.Products;

namespace RetailSystem.Test.Validators
{
    public class UpdateProductCommandValidatorTest
    {
        [Fact]
        public void Should_Not_Have_Error_When_Input_Is_Valid()
        {
            var validator = new UpdateProductCommandValidator();
            var model = CreateValidModel();

            var result = validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Is_Empty()
        {
            var validator = new UpdateProductCommandValidator();
            var model = CreateValidModel();
            model = model with { ProductName = "" };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Too_Long()
        {
            var validator = new UpdateProductCommandValidator();
            var model = CreateValidModel();
            model = model with { ProductName = new string('A', 101) };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ProductName);
        }

        //PRICE

        [Fact]
        public void Should_Have_Error_When_Price_Is_Zero()
        {
            var validator = new UpdateProductCommandValidator();
            var model = CreateValidModel();
            model = model with { Price = 0 };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Negative()
        {
            var validator = new UpdateProductCommandValidator();
            var model = CreateValidModel();
            model = model with { Price = -10 };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        //CATEGOYIDS

        [Fact]
        public void Should_Have_Error_When_CategoryIds_Is_Empty()
        {
            var validator = new UpdateProductCommandValidator();
            var model = CreateValidModel();
            model = model with { CategoryIds = new List<Guid>() };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.CategoryIds);
        }

        //PRODUCTIMAGES
        [Fact]
        public void Should_Have_Error_When_ProductImages_Is_Empty()
        {
            var validator = new UpdateProductCommandValidator();
            var model = CreateValidModel();
            model = model with { ProductImages = new List<ImageUploadDto>() };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ProductImages);
        }

        //SIZEQUANTITY

        [Fact]
        public void Should_Have_Error_When_SizesQuantity_Is_Empty()
        {
            var validator = new UpdateProductCommandValidator();
            var model = CreateValidModel();
            model = model with { SizesQuantity = new List<UploadProductSizeDto>() };

            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.SizesQuantity);
        }
        private UpdateProductCommand CreateValidModel()
        {
            return new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                ProductName = "Product A",
                Price = 100,
                Description = "Desc",
                ThumbnailIndex = 0,
                ThumbnailImageId = null,
                RemoveImageIds = null,

                CategoryIds = new List<Guid>
        {
            Guid.NewGuid()
        },

                ProductImages = new List<ImageUploadDto>
        {
            new ImageUploadDto
            {
                Content = new MemoryStream(new byte[] { 1, 2, 3 }),
                FileName = "img.jpg",
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
        }
    }
}
