using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProcessEndpoint.Interfaces;
using Refit;

namespace ProcessEndpoint.Extensions
{
    public static class RefitExtensions
    {
        public static IServiceCollection ConfigureRefit(this IServiceCollection services, IConfiguration config){

            var settings = new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Converters = { new StringEnumConverter() }
                    })
            };

            services.AddRefitClient<IWorkflowApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(config["Elsa:BaseUrl"]));
            return services;
        }
    }
}