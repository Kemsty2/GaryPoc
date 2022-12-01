namespace ElsaEdiBackend.Extensions.Services;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;

public static class SwaggerServiceExtension
{
    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.CustomSchemaIds(type => type.ToString());
            config.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });

            // note: need a temporary service provider here because one has not been created yet
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            // add a swagger document for each discovered API version
            foreach (var description in provider.ApiVersionDescriptions)
            {
                config.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Version = description.GroupName,
                    Title = "ElsaEdi Api",
                    Description = "Our API uses a REST based design, leverages the JSON data format, and relies upon HTTPS for transport. We respond with meaningful HTTP response codes and if an error occurs, we include error details in the response body.",
                    Contact = new OpenApiContact
                    {
                        Name = "ElsaEdi Api",
                        Email = "it.dev@elsa-edi.com",
                        Url = new Uri("https://www.elsa-edi.com"),
                    },
                });

                config.EnableAnnotations();

                #region Bearer Token

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer token to access this API",
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        }, new List<string>()
                    },
                });

                #endregion Bearer Token

                #region OAuth2 Keycloak

                config.AddSecurityDefinition("OAuth2 Keycloak", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(Environment.GetEnvironmentVariable("AUTH_AUTHORIZATION_URL")),
                            TokenUrl = new Uri(Environment.GetEnvironmentVariable("AUTH_TOKEN_URL")),
                            Scopes = new Dictionary<string, string>
                                {
                                    { Environment.GetEnvironmentVariable("AUTH_AUDIENCE"), "profile roles email openid" }
                                },
                        }
                    },
                    Description = "Keycloak Openapi Scheme"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "OAuth2 Keycloak",
                                },
                            }, Array.Empty<string>()
                        },
                    });

                #endregion OAuth2 Keycloak
            }


            config.IncludeXmlComments(string.Format(@$"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}ElsaEdiBackend.WebApi.xml"));
        });
    }
}