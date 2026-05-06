using RetailSystem.Application.Dtos.ImagesUpload;

namespace RetailSystem.Application.Interfaces.Services
{
    public interface ICloudinaryService
    {
        Task<List<string>?> UploadImages(List<ImageUploadDto> imageUploadDto, string folderName);
    }
}
