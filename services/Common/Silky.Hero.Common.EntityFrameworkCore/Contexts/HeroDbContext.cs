using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Contexts;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.EntityFrameworkCore.MultiTenants.Dependencies;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Rpc.Runtime.Server;

namespace Silky.Hero.Common.EntityFrameworkCore.Contexts;

public abstract class HeroDbContext<TDbContext> : SilkyDbContext<TDbContext>, IModelBuilderFilter, IMultiTenantOnTable
    where TDbContext : DbContext
{
    protected HeroDbContext(DbContextOptions<TDbContext> options) : base(options)
    {
        // 启用实体数据更改监听
        EnabledEntityChangedListener = true;

        // 忽略空值更新
        InsertOrUpdateIgnoreNullValues = true;
    }

    public object GetTenantId()
    {
        return NullSession.Instance.TenantId;
    }

    protected override void SavingChangesEvent(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // 获取当前事件对应上下文
        var dbContext = eventData.Context;
        // 获取所有更改，删除，新增的实体，但排除审计实体（避免死循环）
        var entities = dbContext.ChangeTracker.Entries()
            .Where(u => u.State == EntityState.Modified || u.State == EntityState.Deleted ||
                        u.State == EntityState.Added)
            .ToList();
        if (!entities.Any()) return;

        var session = NullSession.Instance;
        long? userId = session.UserId != null ? long.Parse(session.UserId.ToString()!) : null;
        Guid? tenantId = session.TenantId != null ? Guid.Parse(session.TenantId.ToString()!) : null;
        foreach (var entity in entities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    if (entity.Entity is ICreatedObject createdObject)
                    {
                        createdObject.CreatedTime = DateTimeOffset.Now;
                        createdObject.CreatedBy = userId;
                    }
                    
                    if (entity.Entity is IHasTenantObject tenantObject)
                    {
                        tenantObject.TenantId ??= tenantId;
                    }

                    if (entity.Entity is ISoftDeletedObject deletedObject1)
                    {
                        deletedObject1.IsDeleted = false;
                    }

                    break;
                case EntityState.Deleted:

                    if (entity.Entity is ISoftDeletedObject deletedObject2)
                    {
                        deletedObject2.IsDeleted = true;
                        deletedObject2.DeletedBy = userId;
                        deletedObject2.DeletedTime = DateTimeOffset.Now;
                        entity.State = EntityState.Modified;
                    }

                    break;
                case EntityState.Modified:
                    if (entity.Entity is ICreatedObject)
                    {
                        // 排除创建人
                        entity.Property(nameof(AuditedEntity.CreatedBy)).IsModified = false;
                        // 排除创建日期
                        entity.Property(nameof(AuditedEntity.CreatedTime)).IsModified = false;
                    }

                    if (entity.Entity is IUpdatedObject updatedObject)
                    {
                        updatedObject.UpdatedTime = DateTimeOffset.Now;
                        updatedObject.UpdatedBy = userId;
                    }

                    break;
            }
        }
    }

    public void OnCreating(ModelBuilder modelBuilder, EntityTypeBuilder entityBuilder, DbContext dbContext,
        Type dbContextLocator)
    {
        // 配置租户Id以及假删除过滤器
        LambdaExpression expression = TenantIdAndFakeDeleteQueryFilterExpression(entityBuilder, dbContext);
        if (expression != null)
            entityBuilder.HasQueryFilter(expression);
    }

    protected static LambdaExpression TenantIdAndFakeDeleteQueryFilterExpression(EntityTypeBuilder entityBuilder,
        DbContext dbContext, string onTableTenantId = null, string isDeletedKey = null, object filterValue = null)
    {
        onTableTenantId ??= "TenantId";
        isDeletedKey ??= "IsDeleted";
        IMutableEntityType metadata = entityBuilder.Metadata;
        if (metadata.FindProperty(onTableTenantId) == null && metadata.FindProperty(isDeletedKey) == null)
        {
            return null;
        }

        Expression finialExpression = Expression.Constant(true);
        ParameterExpression parameterExpression = Expression.Parameter(metadata.ClrType, "u");

        // 租户过滤器
        if (entityBuilder.Metadata.ClrType.BaseType.Name == typeof(AuditedEntity).Name)
        {
            if (metadata.FindProperty(onTableTenantId) != null)
            {
                ConstantExpression constantExpression = Expression.Constant(onTableTenantId);
                MethodCallExpression right = Expression.Call(Expression.Constant(dbContext),
                    dbContext.GetType().GetMethod("GetTenantId"));
                finialExpression = Expression.AndAlso(finialExpression, Expression.Equal(Expression.Call(typeof(EF),
                    "Property", new Type[1]
                    {
                        typeof(object)
                    }, parameterExpression, constantExpression), right));
            }
        }

        // 假删除过滤器
        if (metadata.FindProperty(isDeletedKey) != null)
        {
            ConstantExpression constantExpression = Expression.Constant(isDeletedKey);
            ConstantExpression right = Expression.Constant(filterValue ?? false);
            var fakeDeleteQueryExpression = Expression.Equal(Expression.Call(typeof(EF), "Property", new Type[1]
            {
                typeof(bool)
            }, parameterExpression, constantExpression), right);
            finialExpression = Expression.AndAlso(finialExpression, fakeDeleteQueryExpression);
        }

        return Expression.Lambda(finialExpression, parameterExpression);
    }
}