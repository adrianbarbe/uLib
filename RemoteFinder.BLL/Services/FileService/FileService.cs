using RemoteFinder.BLL.Mappers;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Services.FileService;

public class FileService : IFileService
{
    private readonly MainContext _context;
    private readonly IMapper<FileEntity, FileStorage> _fileMapper;

    public FileService(MainContext context, IMapper<FileEntity, FileStorage> fileMapper)
    {
        _context = context;
        _fileMapper = fileMapper;
    }
    
    public List<FileStorage> GetAll()
    {
        return _context.File
            .Select(file => _fileMapper.Map(file))
            .ToList();
    }

    public FileStorage Get(int id)
    {
        var fileEntity = _context.File.FirstOrDefault(f => f.Id == id);

        if (fileEntity == null)
        {
            throw new Exception($"Item not found by the Id {id}");
        }

        return _fileMapper.Map(fileEntity);
    }

    public FileStorage Create(FileStorage fileStorage)
    {
        var fileEntity = _fileMapper.Map(fileStorage);
        
        fileEntity.UserSocialId = fileStorage.UserSocialId;

        fileEntity.CreatedAt = DateTime.UtcNow;
        
        _context.File.Add(fileEntity);
        _context.SaveChanges();

        return fileStorage;
    }

    public FileStorage Update(int id, FileStorage fileStorage)
    {
        var fileEntity = _context.File.FirstOrDefault(f => f.Id == id);

        if (fileEntity == null)
        {
            throw new Exception($"Item not found by the Id {id}");
        }

        fileEntity.FileName = fileStorage.FileName;
        fileEntity.FileSize = fileStorage.FileSize;
        fileEntity.FileType = fileStorage.FileType;

        _context.File.Update(fileEntity);
        _context.SaveChanges();

        return fileStorage;
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