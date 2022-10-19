using Microsoft.AspNetCore.Mvc;
using RemoteFinder.BLL.Services.FileService;
using File = RemoteFinder.Models.File;

namespace RemoteFinder.Web.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public List<File> GetAllFiles()
        {
            return _fileService.GetAll();
        }
    }
}