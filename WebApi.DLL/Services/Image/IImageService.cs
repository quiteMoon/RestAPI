using Microsoft.AspNetCore.Http;

namespace WebApi.BLL.Services.Image
{
    public interface IImageService
    {
        Task<string?> SaveImageAsync(IFormFile image, string directory);
        Task<List<string>> SaveProductImages(List<IFormFile> images, string path);
        void DeleteImage(string filePath);
    }
}
