﻿using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProcessEndpoint.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = (SameSiteMode)(-1);
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters.RoleClaimType = "roles";
                options.TokenValidationParameters.NameClaimType = "name";
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    // this event is fired everytime the cookie has been validated by the cookie middleware,
                    // so basically during every authenticated request
                    // the decryption of the cookie has already happened so we have access to the user claims
                    // and cookie properties - expiration, etc..
                    OnValidatePrincipal = async x =>
                    {
                        // since our cookie lifetime is based on the access token one,
                        // check if we're more than halfway of the cookie lifetime
                        var now = DateTimeOffset.UtcNow;
                        var timeElapsed = now.Subtract(x.Properties.IssuedUtc.Value);
                        var timeRemaining = x.Properties.ExpiresUtc.Value.Subtract(now);

                        if (timeElapsed > timeRemaining)
                        {
                            var identity = (ClaimsIdentity)x.Principal.Identity;
                            var accessTokenClaim = identity.FindFirst("access_token");
                            var refreshTokenClaim = identity.FindFirst("refresh_token");

                            // if we have to refresh, grab the refresh token from the claims, and request
                            // new access token and refresh token
                            var refreshToken = refreshTokenClaim.Value;
                            var response = await new HttpClient().RequestRefreshTokenAsync(new RefreshTokenRequest
                            {
                                Address = config["Jwt:TokenUrl"],
                                ClientId = config["Jwt:ClientId"],
                                ClientSecret = config["Jwt:ClientSecret"],
                                RefreshToken = refreshToken
                            });

                            if (!response.IsError)
                            {
                                // everything went right, remove old tokens and add new ones
                                identity.RemoveClaim(accessTokenClaim);
                                identity.RemoveClaim(refreshTokenClaim);

                                identity.AddClaims(new[]
                                {
                                        new Claim("access_token", response.AccessToken),
                                        new Claim("refresh_token", response.RefreshToken)
                                    });

                                // indicate to the cookie middleware to renew the session cookie
                                // the new lifetime will be the same as the old one, so the alignment
                                // between cookie and access token is preserved
                                x.ShouldRenew = true;
                            }
                        }
                    }
                };
            })
            .AddOpenIdConnect(options =>
            {
                options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;
                options.Authority = config["Jwt:Authority"];
                options.CallbackPath = new PathString("/openid-callback");
                options.ClientId = config["Jwt:ClientId"];
                options.ClientSecret = config["Jwt:ClientSecret"];

                options.RequireHttpsMetadata = false;
                options.SaveTokens = true;

                // openid is already present by default: https://github.com/aspnet/Security/blob/e98a0d243a7a5d8076ab85c3438739118cdd53ff/src/Microsoft.AspNetCore.Authentication.OpenIdConnect/OpenIdConnectOptions.cs#L44-L45
                // adding offline_access to get a refresh token
                options.Scope.Add("offline_access");

                // we want IdSrv to post the data back to us
                options.ResponseMode = OidcConstants.ResponseModes.FormPost;

                // we use the authorisation code flow, so only asking for a code
                options.ResponseType = OidcConstants.ResponseTypes.Code;

                // when the identity has been created from the data we receive,
                // persist it with this authentication scheme, hence in a cookie
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.NonceCookie.SameSite = (SameSiteMode)(-1);
                options.CorrelationCookie.SameSite = (SameSiteMode)(-1);

                // using this property would align the expiration of the cookie
                // with the expiration of the identity token
                // UseTokenLifetime = true,

                options.Events = new OpenIdConnectEvents
                {
                    // that event is called after the OIDC middleware received the auhorisation code,
                    // redeemed it for an access token and a refresh token,
                    // and validated the identity token
                    OnTokenValidated = x =>
                    {
                        // store both access and refresh token in the claims - hence in the cookie
                        var identity = (ClaimsIdentity)x.Principal.Identity;
                        identity.AddClaims(new[]
                        {
                                new Claim("access_token", x.TokenEndpointResponse.AccessToken),
                                new Claim("refresh_token", x.TokenEndpointResponse.RefreshToken)
                            });

                        // so that we don't issue a session cookie but one with a fixed expiration
                        x.Properties.IsPersistent = true;

                        // align expiration of the cookie with expiration of the
                        // access token
                        var accessToken = new JwtSecurityToken(x.TokenEndpointResponse.AccessToken);
                        x.Properties.ExpiresUtc = accessToken.ValidTo;

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
