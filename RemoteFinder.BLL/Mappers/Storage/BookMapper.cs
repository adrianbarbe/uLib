using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Mappers.Storage;

public class BookMapper : IMapper<BookEntity, BookBase>
{
    public BookEntity Map(BookBase source)
    {
        return new BookEntity
        {
            Id = source.Id,
            Name = source.Name,
            CategoryId = source.Category.Id,
            PreviewImageUrl = source.PreviewImageUrl,
        };
    }

    public BookBase Map(BookEntity source)
    {
        return new BookBase
        {
            Id = source.Id,
            Name = source.Name,
            PreviewImageUrl = source.PreviewImageUrl,
            FileName = source.File != null ? source.File.FileName : null,
            Category = source.Category != null
                ? new Category
                {
                    Id = source.Category.Id,
                    Name = source.Category.Name,
                }
                : null,
        };
    }
}