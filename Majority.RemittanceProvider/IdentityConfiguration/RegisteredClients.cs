﻿using IdentityServer4;
using IdentityServer4.Models;
using Majority.RemittanceProvider.IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Majority.RemittanceProvider.IdentityServer.IdentityConfiguration
{
    public class RegisteredClients
    {
       

        public static IEnumerable<Client> Get(IConfiguration config)
        {
            return new List<Client>
        {
            new Client
            {
                    ClientId = config.GetSection("RemittanceProviderConfiguration:ClientId").Value,
                    ClientName = "ASP.NET Core Weather Api",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret(config.GetSection("RemittanceProviderConfiguration:ClientSecret").Value.Sha256())},
                    AllowedScopes = new List<string> {"weatherApi.read"}
            },
            new Client
            {
                ClientId = "oidcMVCApp",
                ClientName = "Sample ASP.NET Core MVC Web App",
                ClientSecrets = new List<Secret> {new Secret("ProCodeGuide".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = new List<string> {"https://localhost:44346/signin-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role",
                    "RemittanceProviderApi.read"
                },

                RequirePkce = true,
                AllowPlainTextPkce = false
            }
        };
        }
    }
}
