using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;
using File = RemoteFinder.Models.File;

namespace RemoteFinder.BLL.Mappers.Storage;

public class FileMapper : IMapper<FileEntity, File>
{
    public FileEntity Map(File source)
    {
        return new FileEntity
        {
            Id = source.Id,
            Name = source.Name,
            FileName = source.FileName,
            FileSize = source.FileSize,
            FileType = source.FileType,
            CreatedAt = source.CreatedAt,
        };
    }

    public File Map(FileEntity source)
    {
        return new File
        {
            Id = source.Id,
            Name = source.Name,
            FileName = source.FileName,
            FileSize = source.FileSize,
            FileType = source.FileType,
            CreatedAt = source.CreatedAt,
        };
    }
}