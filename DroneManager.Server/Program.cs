using DroneManager.Application;
using DroneManager.Core.Data.Helpers;
using DroneManager.Core.Web.Extensions;
using DroneManager.Infrastructure;
using DroneManager.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.AddApplicationLayerServices();
builder.AddWebServices();

// Add middlewares

await using var app = builder.Build();

Task.Run(async () => await app.Services.ApplyPendingChanges<DbContextWrite>(default)).Wait();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerExtension();
}

app.UseErrorHandlingMiddleware();

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
