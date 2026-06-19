using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.StaticFiles;
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
        private IWebAssemblyHostEnvironment _webHostEnviroment;
        public async Task<List<string>> GetUploadedFiles()
        {
            var base64Urls = new List<string>();
            var uploadPath = Path.Combine(_webHostEnviroment.BaseAddress, "uploads");
            var files = Directory.GetFiles(uploadPath);

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

        public Task DownloadFile(string url)
        {
            throw new NotImplementedException();
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
