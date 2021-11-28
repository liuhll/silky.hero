using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Silky.EntityFrameworkCore.Contexts;
using Silky.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Entities;
using Silky.Rpc.Runtime.Server;

namespace Silky.Hero.Common.EntityFrameworkCore.Contexts;

public abstract class HeroContext<TDbContext> : SilkyDbContext<TDbContext>
    where TDbContext : DbContext
{
    protected HeroContext(DbContextOptions<TDbContext> options) : base(options)
    {
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
        if (entities == null || entities.Count < 1) return;

        var session = NullSession.Instance;
        long? userId = session.UserId != null ? long.Parse(session.UserId.ToString()!) : null;
        Guid? tenantId = session.TenantId != null ? Guid.Parse(session.TenantId.ToString()!) : null;
        foreach (var entity in entities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    if (entity.Entity.GetType().IsSubclassOf(typeof(AuditedEntity)))
                    {
                        var obj = entity.Entity as AuditedEntity;
                        var currentTenantId = entity.Property(nameof(Entity.TenantId)).CurrentValue;
                        if (currentTenantId == null)
                            entity.Property(nameof(Entity.TenantId)).CurrentValue = tenantId;

                        obj.CreatedTime = DateTimeOffset.Now;
                        obj.CreatedBy = userId;
                    }

                    if (entity.Entity.GetType().IsSubclassOf(typeof(FullAuditedEntity)))
                    {
                        var obj = entity.Entity as FullAuditedEntity;
                        obj.IsDeleted = false;
                    }

                    break;
                case EntityState.Deleted:

                    if (entity.Entity.GetType().IsSubclassOf(typeof(FullAuditedEntity)))
                    {
                        var obj = entity.Entity as FullAuditedEntity;
                        obj.IsDeleted = true;
                        obj.DeletedBy = userId;
                        obj.DeletedTime = DateTimeOffset.Now;
                        entity.State = EntityState.Modified;
                    }

                    break;
                case EntityState.Modified:
                    if (entity.Entity.GetType().IsSubclassOf(typeof(AuditedEntity)))
                    {
                        // 排除创建人
                        entity.Property(nameof(AuditedEntity.CreatedBy)).IsModified = false;
                        // 排除创建日期
                        entity.Property(nameof(AuditedEntity.CreatedTime)).IsModified = false;
                        var obj = entity.Entity as AuditedEntity;
                        obj.UpdatedTime = DateTimeOffset.Now;
                        obj.UpdatedBy = userId;
                    }

                    break;
            }
        }
    }
}