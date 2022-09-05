using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Runtime.Intrinsics.Arm;

namespace Mango.Services.Identit
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("Mango","Mango Server"),
                new ApiScope(name: "read",displayName: "Read your data"),
                new ApiScope(name: "write",displayName: "Write your data"),
                new ApiScope(name: "delete",displayName: "Delete your data"),

            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                     ClientId = "client",
                     ClientSecrets = {new Secret("secret".Sha256())},
                     AllowedGrantTypes = GrantTypes.ClientCredentials,
                     AllowedScopes = {"read","write","profile"}
                },
                 new Client
                {
                     ClientId = "mo",
                     ClientSecrets = {new Secret("secret".Sha256())},
                     AllowedGrantTypes = GrantTypes.Code,
                     RedirectUris = { "https://localhost:44330/signin-oidc"},
                     PostLogoutRedirectUris = {"https://localhost:44330/signout-callback-oidc"},
                     AllowedScopes = {"read","write","profile"}
                }
            };
    }
}
