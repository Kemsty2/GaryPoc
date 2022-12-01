using ElsaEdiBackend.Databases;
using ElsaEdiBackend.Extensions.Application;
using ElsaEdiBackend.Extensions.Host;
using ElsaEdiBackend.Extensions.Services;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddLoggingConfiguration(builder.Environment);

builder.ConfigureServices();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using var scope = app.Services.CreateScope();
var initializer = scope.ServiceProvider.GetRequiredService<DatabaseHelper>();
//await initializer.MigrateAsync();
await initializer.SeedAsync();

// For elevated security, it is recommended to remove this middleware and set your server to only listen on https.
// A slightly less secure option would be to redirect http to 400, 505, etc.
app.UseAutoWrapper();
app.UseHttpsRedirection();

app.UseCors("ElsaEdiApiCorsPolicy");

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/api/health");
    endpoints.MapControllers();
});

app.UseSwaggerExtension(apiVersionDescriptionProvider);

try
{
    Log.Information("Starting application");
    await app.RunAsync();
}
catch (Exception e)
{
    Log.Error(e, "The application failed to start correctly");
    throw;
}
finally
{
    Log.Information("Shutting down application");
    Log.CloseAndFlush();
}

app.Run();

// Make the implicit Program class public so the functional test project can access it
public partial class Program
{ }