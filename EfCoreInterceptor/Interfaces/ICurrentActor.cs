namespace EfCoreInterceptor.Interfaces;


public interface ICurrentActor
{
    string? UserId { get; }
    Guid? TenantId { get; }
    string CorrelationId { get; }
}
