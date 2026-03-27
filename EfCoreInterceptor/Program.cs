using EfCoreInterceptor.Data;
using EfCoreInterceptor.Interfaces;
using EfCoreInterceptor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentActor, CurrentActor>();
// Interceptors: usually safe as singleton if they are stateless.
// Ours depends on scoped ICurrentActor, so we register them as scoped.
builder.Services.AddScoped<AuditAndSoftDeleteInterceptor>();
builder.Services.AddScoped<ObservabilityCommandInterceptor>();
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // Add interceptors from DI
    options.AddInterceptors(
    sp.GetRequiredService<AuditAndSoftDeleteInterceptor>(),
    sp.GetRequiredService<ObservabilityCommandInterceptor>()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.MapPost("/invoice", async ([FromBody] Invoice invoice, ApplicationDbContext  application) =>
{
   await application.Invoices.AddAsync(invoice);
    await application.SaveChangesAsync();
    return Results.Ok();
})
.WithName("addinvoice");

app.MapGet("/invoices", async (ApplicationDbContext application) =>
{
    var invs = await application.Invoices.ToListAsync();

    return Results.Ok(invs);
});

app.MapGet("/delete{id}", async (Guid id, ApplicationDbContext application) =>
{
    var inv = await application.Invoices.FindAsync(id);

    application.Invoices.Remove(inv!);

    await application.SaveChangesAsync();

    return Results.Ok();
});

app.Run();


