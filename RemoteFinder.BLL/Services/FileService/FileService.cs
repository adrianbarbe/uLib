using RemoteFinder.BLL.Mappers;
using RemoteFinder.BLL.Services.AuthorizationService;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Services.FileService;

public class FileService : IFileService
{
    private readonly MainContext _context;
    private readonly IMapper<FileEntity, FileStorage> _fileMapper;
    private readonly IAuthorizationService _authorizationService;

    public FileService(MainContext context, 
        IMapper<FileEntity, FileStorage> fileMapper,
        IAuthorizationService authorizationService)
    {
        _context = context;
        _fileMapper = fileMapper;
        _authorizationService = authorizationService;
    }

    public FileStorage Get(int id)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();

        var fileEntity = _context.File.FirstOrDefault(f => f.Id == id && f.UserSocialId == currentUserId);

        if (fileEntity == null)
        {
            throw new Exception($"Item not found by the Id {id}");
        }

        return _fileMapper.Map(fileEntity);
    }

    public FileStorage Create(FileStorage fileStorage)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();
        
        var fileEntity = _fileMapper.Map(fileStorage);
        
        fileEntity.UserSocialId = currentUserId;

        _context.File.Add(fileEntity);
        _context.SaveChanges();

        return fileStorage;
    }

    public void Delete(int id)
    {
        var currentUserId = _authorizationService.GetCurrentUserId();

        var fileEntity = _context.File.FirstOrDefault(f => f.Id == id && f.UserSocialId == currentUserId);

        if (fileEntity == null)
        {
            throw new Exception($"Item not found by the Id {id}");
        }

        _context.Remove(fileEntity);
        _context.SaveChanges();
    }
}