using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteFinder.DAL.Contants;
using RemoteFinder.Entities.Storage;

namespace RemoteFinder.DAL.EntityConfigurations;

public class FileEntityConfig : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> entity)
    {
        entity.ToTable(TableName.File);
    }
}