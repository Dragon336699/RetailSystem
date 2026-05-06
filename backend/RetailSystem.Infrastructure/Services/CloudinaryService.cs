using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using RetailSystem.Application.Dtos.ImagesUpload;
using RetailSystem.Application.Interfaces.Services;

namespace RetailSystem.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration _configuration;
        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<string>?> UploadImages(List<ImageUploadDto> imageUploadDto, string folderName)
        {
            var uploadUrls = new List<string>();
            var cloudinaryApi = _configuration["Cloudinary:Api"];

            var cloudinary = new Cloudinary(cloudinaryApi);
            cloudinary.Api.Secure = true;

            foreach (var fileInfo in imageUploadDto)
            {
                UploadResult? uploadResult = null;

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileInfo.FileName, fileInfo.Content),
                    Folder = folderName,
                    UseFilename = false,
                    UniqueFilename = true,
                };

                uploadResult = await cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    return null;
                }
                if (uploadResult != null) uploadUrls.Add(uploadResult.SecureUrl.ToString());
            }

            return uploadUrls;
        }
    }
}
