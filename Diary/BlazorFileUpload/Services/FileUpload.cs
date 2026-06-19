using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorFileUpload.Services
{
    public interface IFileUpload
    {
        Task UploadFile(IBrowserFile file);
        Task<string> GeneratePreviewUrl(IBrowserFile file);
    }
    public class FileUpload : IFileUpload
    {

        private  IWebAssemblyHostEnvironment _webHostEnviroment;
        private readonly ILogger<FileUpload> _logger;

        public FileUpload(IWebAssemblyHostEnvironment webAssemblyHostEnvironment, 
            ILogger<FileUpload> logger)
        {
            _logger = logger;
            _webHostEnviroment = webAssemblyHostEnvironment;
        }


        public async Task UploadFile(IBrowserFile file)
        {
            if (file != null) {
                try
                {

                    var uploadPath = Path.Combine(_webHostEnviroment.BaseAddress, "uploads", file.Name);

                    using (var stream = file.OpenReadStream())
                    {
                        var fileStream = File.Create(uploadPath);
                        await stream.CopyToAsync(fileStream);
                        fileStream.Close();
                    }
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.ToString());
                }
            }
        }

        public async Task<string> GeneratePreviewUrl(IBrowserFile file)
        {
            if(!file.ContentType.Contains("image"))
            {
                if (!file.ContentType.Contains("pdf"))
                {
                    return "images/pdf_logo.png";
                }
            }

            // Generating preview URL
            var resizedImage = await file.RequestImageFileAsync(file.ContentType, 100 , 100);
            var buffer = new byte[resizedImage.Size];
            await resizedImage.OpenReadStream().ReadAsync(buffer);
            return $"Data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";

        }
    }
}
