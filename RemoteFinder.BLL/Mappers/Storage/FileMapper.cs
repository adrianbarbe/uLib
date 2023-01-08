using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Mappers.Storage;

public class FileMapper : IMapper<FileEntity, FileStorage>
{
    public FileEntity Map(FileStorage source)
    {
        return new FileEntity
        {
            Id = source.Id,
            FileName = source.FileName,
            FileSize = source.FileSize,
            FileType = source.FileType,
            CreatedAt = source.CreatedAt,
        };
    }

    public FileStorage Map(FileEntity source)
    {
        return new FileStorage
        {
            Id = source.Id,
            FileName = source.FileName,
            FileSize = source.FileSize,
            FileType = source.FileType,
            CreatedAt = source.CreatedAt,
        };
    }
}