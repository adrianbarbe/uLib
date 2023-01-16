using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Mappers.Storage;

public class CategoryMapper : IMapper<CategoryEntity, Category>
{
    private readonly IMapper<BookEntity, BookBase> _bookMapper;

    public CategoryMapper(IMapper<BookEntity, BookBase> bookMapper)
    {
        _bookMapper = bookMapper;
    }
    public CategoryEntity Map(Category source)
    {
        return new CategoryEntity
        {
            Id = source.Id,
            Name = source.Name,
        };
    }

    public Category Map(CategoryEntity source)
    {
        return new Category
        {
            Id = source.Id,
            Name = source.Name,
            Books = source.Books?.Select(b => _bookMapper.Map(b)).ToList(),
        };
    }
}