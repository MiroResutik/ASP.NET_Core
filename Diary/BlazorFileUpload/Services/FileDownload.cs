using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.JSInterop;
using System.Buffers.Text;

namespace BlazorFileUpload.Services
{

    public interface IFileDownload
    {
        Task<List<String>> GetUploadedFiles();
        Task DownloadFile(string url);

        
    }
    public class FileDownload : IFileDownload
    {
        public class UploadController : ControllerBase
        {
            private readonly IWebHostEnvironment _env;

            public UploadController(IWebHostEnvironment env)
            {
                _env = env;
            }

            [HttpPost]
            public async Task<IActionResult> Upload(IFormFile file)
            {
                var path = Path.Combine(
                    _env.WebRootPath,
                    "uploads",
                    file.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                return Ok();
            }
        }
        private IWebAssemblyHostEnvironment _webHostEnviroment;

        private readonly IJSRuntime _js;

        //public FileDownload(IWebHostEnvironment webHostEnvironment, IJSRuntime js)
        //{
        //    _webHostEnviroment = webHostEnvironment;
        //    _js = js;
        //}
        public async Task<List<string>> GetUploadedFiles()
        {
            var base64Urls = new List<string>();
            var downloadPath = Path.Combine(_webHostEnviroment.Environment, "uploads");
            var files = Directory.GetFiles(downloadPath);

            if (files is not null && files.Length > 0)
            {
                foreach (var file in files) 
                {
                    using (var fileInput = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        var memoryStream = new MemoryStream();
                        await fileInput.CopyToAsync(memoryStream);
                        var buffer = memoryStream.ToArray();
                        var fileType = GetMimeTypeForFileExtension(file);
                        // Adds url of the file
                        base64Urls.Add($"data:{ fileType}; base64,{Convert.ToBase64String(buffer)}");
                    }
                }

            }
            return base64Urls;
        }

        public async Task DownloadFile(string url)
        {
            await _js.InvokeVoidAsync("downloadFile", url);
        }
        private string GetMimeTypeForFileExtension(string filePath)
        {
            const string DefaultContentType = "application/octet-stream";   
            
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(filePath, out string contentType))
            {
                return DefaultContentType;
            }
            return contentType;
        }
    }
}
