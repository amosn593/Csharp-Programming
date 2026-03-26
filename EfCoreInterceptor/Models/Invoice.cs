using EfCoreInterceptor.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EfCoreInterceptor.Models;

public sealed class Invoice : IAuditableEntity, ISoftDelete
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    [Precision(18,2)]
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "EUR";
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }

}
