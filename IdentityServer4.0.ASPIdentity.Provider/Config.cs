// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;

namespace IdentityServer4._0.ASPIdentity.Provider
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("IdentityServer4Provider", "IdentityServer4 home implementation") };

        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client()
            {
                ClientId = "IdentityServer4CliClient",

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets = new Secret[]
                {
                    new Secret("secret".ToSha256())
                },

                AllowedScopes = { "IdentityServer4Provider" }
            },
            new Client()
            {
                ClientId = "IdentityServer4WebClient",

                ClientSecrets = new Secret[]
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.Code,
                AllowOfflineAccess = true,

                RedirectUris = new string[] { "https://localhost:5002/signin-oidc" },

                PostLogoutRedirectUris = new string[] { "https://localhost:5002/signout-callback-oidc" },

                AllowedScopes = new string[]
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "IdentityServer4Provider",

                    // Must not be used in the implicit flow!
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                }
            },
            new Client()
            {
                ClientId = "IdentityServer4JSClient",
                ClientName = "JS Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,

                RedirectUris = new string[] { "https://localhost:5003/callback.html" },
                PostLogoutRedirectUris = new string[] { "https://localhost:5003/index.html" },
                AllowedCorsOrigins = new string[] { "https://localhost:5003" },

                AllowedScopes = new string[]
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "IdentityServer4Provider"
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