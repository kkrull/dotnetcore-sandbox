using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
  static class Config
  {
    public static IEnumerable<ApiResource> GetApiResources()
    {
      return new List<ApiResource>
      {
        new ApiResource("api1", "My API")
      };
    }

    public static IEnumerable<Client> GetClients()
    {
      return new List<Client>
      {
        //Non-interactive using clientid/secret for authentication
        new Client
        {
          ClientId = "client",
          AllowedGrantTypes = GrantTypes.ClientCredentials,
          AllowedScopes = { "api1" },
          ClientSecrets = { new Secret("secret".Sha256()) },
        },

        //OpenID Connect implicit flow client
        new Client
        {
          ClientId = "mvc",
          AllowedGrantTypes = GrantTypes.Implicit,
          AllowedScopes = new List<string>
          {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
          },
          
          ClientName = "MVC Client",
          RedirectUris = { "http://localhost:5002/signin-oidc" },
          RequireConsent = false,
          PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
        },

        //Resource owner with a password
        new Client
        {
          ClientId = "ro.client",
          AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
          AllowedScopes = { "api1" },
          ClientSecrets = { new Secret("secret".Sha256()) },
        },
        
        //OpenID Connect authorization code
        new Client
        {
          ClientId = "tableau-permission-grant",
          AllowedGrantTypes = GrantTypes.Code,
          AllowedScopes = new List<string>
          {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
          },
          
          ClientName = "Authorization Code",
          ClientSecrets = { new Secret("secret".Sha256()) },
          RedirectUris = { "https://oidcdebugger.com/debug" },
          RequireConsent = false,
          PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
        },
      };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
      return new List<IdentityResource>
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
      };
    }

    public static List<TestUser> GetUsers()
    {
      return new List<TestUser>
      {
        new TestUser
        {
          SubjectId = "1",
          Username = "alice",
          Password = "password"
        },
        new TestUser
        {
          SubjectId = "2",
          Username = "bob",
          Password = "password"
        }
      };
    }
  }
}