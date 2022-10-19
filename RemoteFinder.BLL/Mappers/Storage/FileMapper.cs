using RemoteFinder.Entities.Storage;
using File = RemoteFinder.Models.File;

namespace RemoteFinder.BLL.Mappers.Storage;

public class FileMapper : IMapper<FileEntity, File>
{
    public FileEntity Map(File source)
    {
        return new FileEntity(source.Name, source.FileName, source.FileType, source.FileSize);
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