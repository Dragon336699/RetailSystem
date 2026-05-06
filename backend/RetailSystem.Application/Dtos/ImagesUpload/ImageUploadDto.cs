namespace RetailSystem.Application.Dtos.ImagesUpload
{
    public class ImageUploadDto
    {
        public required Stream Content { get; init; }
        public required string FileName { get; init; }
        public required string ContentType { get; init; }
    }
}
