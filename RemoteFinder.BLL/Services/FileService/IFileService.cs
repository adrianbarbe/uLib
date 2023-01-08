using RemoteFinder.Models;

namespace RemoteFinder.BLL.Services.FileService;

public interface IFileService
{
    FileStorage Get(int id);

    FileStorage Create(FileStorage fileStorage);
    
    void Delete(int id);
}