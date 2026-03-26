using EfCoreInterceptor.Interfaces;
using System.Diagnostics;

namespace EfCoreInterceptor.Models;

public sealed class CurrentActor : ICurrentActor
{
    public CurrentActor(IHttpContextAccessor accessor)
    {
        var http = accessor.HttpContext;
        UserId = http?.User?.Identity?.Name??"System";
        TenantId = Guid.TryParse(http?.Request.Headers["X-Tenant-Id"], out var tid) ? tid : null;
        CorrelationId =
        http?.TraceIdentifier
        ?? Activity.Current?.Id
        ?? Guid.NewGuid().ToString("N");
    }
    public string? UserId { get; }
    public Guid? TenantId { get; }
    public string CorrelationId { get; }
}
