using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ProcessEndpoint.Extensions
{
    public static class LoggingConfiguration
    {
        public static void AddLoggingConfiguration(this IHostBuilder host, IWebHostEnvironment env)
        {
            var loggingLevelSwitch = new LoggingLevelSwitch();
            if (env.IsDevelopment())
                loggingLevelSwitch.MinimumLevel = LogEventLevel.Warning;
            if (env.IsProduction())
                loggingLevelSwitch.MinimumLevel = LogEventLevel.Information;

            var logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()                
                .Enrich.WithProperty("ApplicationName", env.ApplicationName)                
                .WriteTo.Console();

            Log.Logger = logger.CreateLogger();

            host.UseSerilog();
        }
    }
}
