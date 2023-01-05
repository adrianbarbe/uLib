using ePlato.CoreApp.BLL.AwsMinioClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteFinder.BLL.Exceptions;
using RemoteFinder.BLL.Services.AwsMinioClient;

namespace RemoteFinder.Web.Controllers
{
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IAwsMinioClient _awsMinioClient;

        public FileController(IWebHostEnvironment environment, IAwsMinioClient awsMinioClient)
        {
            _environment = environment;
            _awsMinioClient = awsMinioClient;
        }
        
        
        [HttpPost]
        // [Authorize]
        [Route("upload/book")]
        public async Task<string> UploadBook(IFormFile file)
        {
            using (var fileMemoryStream = new MemoryStream())
            {
                var fileSize = 30 * 1024 * 1024;
                if (file.Length > fileSize) // Max file size 30Mb
                {
                    throw new UploadFileException($"File size must bt less than {fileSize / 1024} kb");
                }
                
                file.CopyTo(fileMemoryStream);
                
                var fileName = await _awsMinioClient.Upload(MinioBucketNames.UlibBooks, fileMemoryStream,file.FileName);
                
                return fileName;
            }
        }
        
        [HttpGet]
        [Route("get/book/{fileName}")]
        public async Task<ActionResult> GetBook(string fileName)
        {
            var responseFile = await _awsMinioClient.Get(MinioBucketNames.UlibBooks, fileName);

            return new FileStreamResult(responseFile.Stream, responseFile.ContentType);
        }
    }
}