using File = RemoteFinder.Models.File;

namespace RemoteFinder.BLL.Services.FileService;

public interface IFileService
{ 
    List<File> GetAll();
}