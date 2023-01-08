using RemoteFinder.Models;

namespace RemoteFinder.BLL.Services.FileService;

public interface IFileService
{ 
    List<FileStorage> GetAll();

    FileStorage Get(int id);

    FileStorage Create(FileStorage fileStorage);

    FileStorage Update(int id, FileStorage fileStorage);

    void Delete(int id);
}