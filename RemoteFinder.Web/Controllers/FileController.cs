using ePlato.CoreApp.BLL.AwsMinioClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteFinder.BLL.Exceptions;
using RemoteFinder.BLL.Services.AwsMinioClient;
using RemoteFinder.BLL.Services.FileService;
using RemoteFinder.Models;
using RemoteFinder.Models.Constants;

namespace RemoteFinder.Web.Controllers
{
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IAwsMinioClient _awsMinioClient;
        private readonly IFileService _fileService;

        public FileController(IWebHostEnvironment environment, 
            IAwsMinioClient awsMinioClient,
            IFileService fileService)
        {
            _environment = environment;
            _awsMinioClient = awsMinioClient;
            _fileService = fileService;
        }
        
        
        [HttpPost]
        [Authorize(Policy = Policies.User)]
        [Route("upload/book")]
        public async Task<FileStorage> UploadBook(IFormFile file)
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

                var fileStorage = new FileStorage
                {
                    FileName = fileName,
                    FileSize = file.Length,
                    FileType = Path.GetExtension(fileName),
                };
                var fileStorageCreated = _fileService.Create(fileStorage);
                
                return fileStorageCreated;
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