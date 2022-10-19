using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteFinder.DAL.Contants;
using RemoteFinder.Entities.Payments;

namespace RemoteFinder.DAL.EntityConfigurations;

public class PaymentEntityConfig : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> entity)
    {
        entity.ToTable(TableName.Payment);
    }
}