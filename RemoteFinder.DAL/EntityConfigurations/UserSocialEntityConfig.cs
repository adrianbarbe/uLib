using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteFinder.DAL.Contants;
using RemoteFinder.Entities.Authentication;

namespace RemoteFinder.DAL.EntityConfigurations;

public class UserSocialEntityConfig : IEntityTypeConfiguration<UserSocialEntity>
{
    public void Configure(EntityTypeBuilder<UserSocialEntity> entity)
    {
        entity.ToTable(TableName.UserSocial);
    }
}