using AutoWrapper;

namespace ElsaEdiBackend.Extensions.Application
{
    public static class AutoWrapperExtension
    {
        public static IApplicationBuilder UseAutoWrapper(this IApplicationBuilder app)
        {
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                ShouldLogRequestData = false,
                UseApiProblemDetailsException = false,
                EnableResponseLogging = false,
                EnableExceptionLogging = true,
                IsApiOnly = false
            });
            return app;
        }
    }
}
