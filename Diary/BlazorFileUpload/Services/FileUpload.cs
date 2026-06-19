using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorFileUpload.Services
{
    public interface IFileUpload
    {
        Task UploadFile(IBrowserFile file);
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
    }
}
