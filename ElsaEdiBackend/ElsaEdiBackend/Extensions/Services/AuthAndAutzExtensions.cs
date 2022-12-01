using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace ElsaEdiBackend.Extensions.Services
{
    public static class AuthAndAutzExtensions
    {
        public static void AddAuthAndAutzServices(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.Authority = Environment.GetEnvironmentVariable("AUTH_AUTHORITY");
                    o.Audience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE");

                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidIssuer = Environment.GetEnvironmentVariable("AUTH_AUTHORITY"),
                        ValidAudience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE")
                    };

                    o.BackchannelHttpHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = delegate { return true; }
                    };
                });

            services.AddAuthorization();

            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
        }
    }

    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            if (context.User.Identity is not { IsAuthenticated: true })
            {
                context.Fail();
                return Task.CompletedTask;
            }

            bool validRole;
            if (!requirement.AllowedRoles.Any())
            {
                validRole = true;
            }
            else
            {
                var roles = requirement.AllowedRoles;

                validRole = GetUserRoles(context).Intersect(roles).Any();
            }

            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }

        #region Private Methods

        private static IEnumerable<string> GetUserRoles(AuthorizationHandlerContext context)
        {
            if (!context.User.HasClaim(c => c.Type == "resource_access")) return new List<string>();

            var claim = context.User.FindFirst("resource_access");
            if (claim == null)
                return new List<string>();

            var json = JObject.Parse(claim.Value);
            var content = json[Environment.GetEnvironmentVariable("AUTH_CLIENT_ID")]?["roles"];
            if (content != null)
            {
                return content.ToObject<List<string>>() ?? new List<string>();
            }

            return new List<string>();
        }

        #endregion Private Methods
    }
}
