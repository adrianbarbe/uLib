using RemoteFinder.Entities.Storage;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Mappers.Storage;

public class CategoryMapper : IMapper<CategoryEntity, Category>
{
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
        };
    }
}