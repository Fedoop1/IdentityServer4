﻿using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer4
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("IdentityServer4Provider", "IdentityServer4 home implementation") };

        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client()
            {
                ClientId = "IdentityServer4Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets = new Secret[] 
                {
                    new Secret("secret".ToSha256())
                },

                AllowedScopes = { "IdentityServer4Provider" }
            },
            new Client()
            {
                ClientId = "mvc",

                ClientSecrets = new Secret[]
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = new string[] { "https://localhost:5002/signin-oidc" },

                PostLogoutRedirectUris = new string[] { "https://localhost:5002/signout-callback-oidc" },

                AllowedScopes = new string[]
                {
                    IdentityServerConstants.StandardScopes.OpenId, 
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] 
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
    }
}