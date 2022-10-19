using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteFinder.DAL.Contants;
using RemoteFinder.Entities.Authentication;

namespace RemoteFinder.DAL.EntityConfigurations;

public class UserAdminEntityConfig : IEntityTypeConfiguration<UserAdminEntity>
{
    public void Configure(EntityTypeBuilder<UserAdminEntity> entity)
    {
        entity.ToTable(TableName.UserAdmin);
    }
}