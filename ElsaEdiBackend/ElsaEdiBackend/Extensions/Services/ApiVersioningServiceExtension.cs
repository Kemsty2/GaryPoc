namespace ElsaEdiBackend.Extensions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

public static class ApiVersioningServiceExtension
{
    public static void AddApiVersioningExtension(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            // Default API Version
            config.DefaultApiVersion = new ApiVersion(1, 0);
            // use default version when version is not specified
            config.AssumeDefaultVersionWhenUnspecified = true;
            // Advertise the API versions supported for the particular endpoint
            config.ReportApiVersions = true;

            config.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("api-version"));
        });

        // Add VersionedApiExplorer()
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}