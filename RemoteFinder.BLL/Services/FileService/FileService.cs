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
}