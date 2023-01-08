using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteFinder.DAL.Contants;
using RemoteFinder.Entities.Storage;

namespace RemoteFinder.DAL.EntityConfigurations;

public class BookEntityConfig : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> entity)
    {
        entity.ToTable(TableName.Book);
    }
}