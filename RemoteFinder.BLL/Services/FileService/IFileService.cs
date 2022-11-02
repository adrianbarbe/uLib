using File = RemoteFinder.Models.File;

namespace RemoteFinder.BLL.Services.FileService;

public interface IFileService
{ 
    List<File> GetAll();

    File Get(int id);

    File Create(File file);

    File Update(int id, File file);

    void Delete(int id);
}