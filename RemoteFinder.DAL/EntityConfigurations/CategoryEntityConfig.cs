using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteFinder.DAL.Contants;
using RemoteFinder.Entities.Storage;

namespace RemoteFinder.DAL.EntityConfigurations;

public class CategoryEntityConfig : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> entity)
    {
        entity.ToTable(TableName.Category);
    }
}