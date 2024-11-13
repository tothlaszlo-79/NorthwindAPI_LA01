using Microsoft.AspNetCore.Authentication;
using NorthwindAPI.Auth;

namespace NorthwindAPI_LA01.Extensions
{
    public static class AuthExtension
    {
        public static AuthenticationBuilder AddApiKeySupport
            (this AuthenticationBuilder builder, Action<ApiKeyAuthenticationOptions> options)
        {
            return builder.AddScheme<ApiKeyAuthenticationOptions, 
                ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.DefaultScheme ,options);
        }
    }
}
