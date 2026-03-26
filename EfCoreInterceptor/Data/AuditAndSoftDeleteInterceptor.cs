using EfCoreInterceptor.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EfCoreInterceptor.Data;

public sealed class AuditAndSoftDeleteInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentActor _actor;
    public AuditAndSoftDeleteInterceptor(ICurrentActor actor)
    => _actor = actor;
    public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result)
    {
        ApplyRules(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        ApplyRules(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private void ApplyRules(DbContext? context)
    {
        if (context is null) return;
        var now = DateTimeOffset.UtcNow;
        var user = _actor.UserId ?? "system";
        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is IAuditableEntity auditable)
            {
                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedAt = now;
                    auditable.CreatedBy = user;
                }
                if (entry.State is EntityState.Added or EntityState.Modified)
                {
                    auditable.UpdatedAt = now;
                    auditable.UpdatedBy = user;
                }
            }
            if (entry.Entity is ISoftDelete softDelete && entry.State == EntityState.Deleted)
            {
                // Convert hard delete into soft delete
                entry.State = EntityState.Modified;
                softDelete.IsDeleted = true;
                softDelete.DeletedAt = now;
                softDelete.DeletedBy = user;
                // Also counts as an update
                if (entry.Entity is IAuditableEntity a)
                {
                    a.UpdatedAt = now;
                    a.UpdatedBy = user;
                }
            }
        }
    }
}
