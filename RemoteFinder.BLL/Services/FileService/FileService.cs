using RemoteFinder.BLL.Mappers;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using File = RemoteFinder.Models.File;

namespace RemoteFinder.BLL.Services.FileService;

public class FileService : IFileService
{
    private readonly MainContext _context;
    private readonly IMapper<FileEntity, File> _fileMapper;

    public FileService(MainContext context, IMapper<FileEntity, File> fileMapper)
    {
        _context = context;
        _fileMapper = fileMapper;
    }
    
    public List<File> GetAll()
    {
        return _context.File
            .Select(file => _fileMapper.Map(file))
            .ToList();
    }

    public File Get(int id)
    {
        var fileEntity = _context.File.FirstOrDefault(f => f.Id == id);

        if (fileEntity == null)
        {
            throw new Exception($"Item not found by the Id {id}");
        }

        return _fileMapper.Map(fileEntity);
    }

    public File Create(File file)
    {
        var fileEntity = _fileMapper.Map(file);
        
        fileEntity.UserSocialId = file.UserSocialId;

        fileEntity.CreatedAt = DateTime.UtcNow;
        
        _context.File.Add(fileEntity);
        _context.SaveChanges();

        return file;
    }

    public File Update(int id, File file)
    {
        var fileEntity = _context.File.FirstOrDefault(f => f.Id == id);

        if (fileEntity == null)
        {
            throw new Exception($"Item not found by the Id {id}");
        }

        fileEntity.Name = file.Name;
        fileEntity.FileName = file.FileName;
        fileEntity.FileSize = file.FileSize;
        fileEntity.FileType = file.FileType;

        _context.File.Update(fileEntity);
        _context.SaveChanges();

        return file;
    }

    public void Delete(int id)
    {
        var fileEntity = _context.File.FirstOrDefault(f => f.Id == id);

        if (fileEntity == null)
        {
            throw new Exception($"Item not found by the Id {id}");
        }

        _context.Remove(fileEntity);
        _context.SaveChanges();
    }
}