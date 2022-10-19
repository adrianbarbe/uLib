using Microsoft.EntityFrameworkCore;
using RemoteFinder.DAL.EntityConfigurations;
using RemoteFinder.Entities.Authentication;
using RemoteFinder.Entities.Payments;
using RemoteFinder.Entities.Storage;

namespace RemoteFinder.DAL;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options) {}
    
    public virtual DbSet<UserSocialEntity> UserSocial { get; set; }
    public virtual DbSet<UserAdminEntity> UserAdmin { get; set; }
    public virtual DbSet<FileEntity> File { get; set; }
    public virtual DbSet<PaymentEntity> Payment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserSocialEntityConfig());
        modelBuilder.ApplyConfiguration(new UserAdminEntityConfig());
        modelBuilder.ApplyConfiguration(new FileEntityConfig());
        modelBuilder.ApplyConfiguration(new PaymentEntityConfig());
    }
}