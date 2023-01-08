using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RemoteFinder.DAL.EntityConfigurations;
using RemoteFinder.DAL.Helpers;
using RemoteFinder.Entities;
using RemoteFinder.Entities.Authentication;
using RemoteFinder.Entities.Payments;
using RemoteFinder.Entities.Storage;

namespace RemoteFinder.DAL;

public class MainContext : DbContext
{
    private readonly IAuthorizationDAHelper _authorizationDaHelper;
    public MainContext(DbContextOptions<MainContext> options, IAuthorizationDAHelper authorizationDaHelper) : base(options)
    {
        _authorizationDaHelper = authorizationDaHelper;
    }
    
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    
    public virtual DbSet<UserSocialEntity> UserSocial { get; set; }
    public virtual DbSet<UserAdminEntity> UserAdmin { get; set; }
    public virtual DbSet<FileEntity> File { get; set; }
    public virtual DbSet<PaymentEntity> Payment { get; set; }
    
    public virtual DbSet<BookEntity> Book { get; set; }
    public virtual DbSet<CategoryEntity> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseSoftDelete).IsAssignableFrom(entity.ClrType))
            {
                modelBuilder
                    .Entity(entity.ClrType)
                    .HasQueryFilter(GetIsDeletedRestriction(entity.ClrType));
            }
        }
        
        modelBuilder.ApplyConfiguration(new UserSocialEntityConfig());
        modelBuilder.ApplyConfiguration(new UserAdminEntityConfig());
        modelBuilder.ApplyConfiguration(new FileEntityConfig());
        modelBuilder.ApplyConfiguration(new PaymentEntityConfig());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfig());
        modelBuilder.ApplyConfiguration(new BookEntityConfig());
    }
    
    // Methods for soft delete
    private static readonly MethodInfo PropertyMethod = typeof(EF)
        .GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public)
        .MakeGenericMethod(typeof(bool));
    private static LambdaExpression GetIsDeletedRestriction(Type type)
    {
        var parm = Expression.Parameter(type, "it");
        var prop = Expression.Call(PropertyMethod, parm, Expression.Constant(nameof(BaseSoftDelete.IsDeleted)));
        var condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
        var lambda = Expression.Lambda(condition, parm);

        return lambda;
    }
    private void OnBeforeSaving()
    {
        foreach (var entry in ChangeTracker.Entries<BaseSoftDelete>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedBy = _authorizationDaHelper.GetUserName();
                    entry.CurrentValues[nameof(BaseSoftDelete.IsDeleted)] = true;
                    entry.State = EntityState.Modified;
                    break;
            }
        }
    }
}