using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var apiClient = new ApiClient("http://localhost:5000", "http://localhost:5001/identity");
      var clientId = args.FirstOrDefault() ?? "client";
      switch (clientId)
      {
        case "client":
        {
          var response = apiClient.NonInteractiveRequestAsync("api1").GetAwaiter().GetResult();
          Console.WriteLine(response);
          return;
        }
        case "ro.client":
        {
          var response = apiClient.PasswordRequestAsync("api1").GetAwaiter().GetResult();
          Console.WriteLine(response);
          return;
        }
        default:
          throw new ArgumentException($"Unknown client: {clientId}");
      }
    }
  }

  sealed class ApiClient
  {
    readonly string _identityServerUrl;
    readonly string _apiServerUrl;
    
    public ApiClient(string identityServerUrl, string apiServerUrl)
    {
      _identityServerUrl = identityServerUrl;
      _apiServerUrl = apiServerUrl;
    }

    public async Task<JArray> NonInteractiveRequestAsync(string scope)
    {
      var accessTokenUrl = await DiscoverAccessTokenEndpointAsync();
      var tokenClient = new TokenClient(accessTokenUrl, "client", "secret");
      var tokenResponse = await RequestAccessTokenAsync(tokenClient, scope);
      var accessToken = tokenResponse.TryGetString("access_token");
      var apiResponse = await MakeApiCallAsync(accessToken);
      return JArray.Parse(apiResponse);
    }

    public async Task<JArray> PasswordRequestAsync(string scope)
    {
      var accessTokenUrl = await DiscoverAccessTokenEndpointAsync();
      var tokenClient = new TokenClient(accessTokenUrl, "ro.client", "secret");
      var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", scope);
      var accessToken = tokenResponse.Json.TryGetString("access_token");
      var apiResponse = await MakeApiCallAsync(accessToken);
      return JArray.Parse(apiResponse);
    }

    async Task<string> DiscoverAccessTokenEndpointAsync()
    {
      var discoveryDocument = await DiscoveryClient.GetAsync(_identityServerUrl);
      if (discoveryDocument.IsError)
      {
        throw new InvalidOperationException(discoveryDocument.Error);
      }

      return discoveryDocument.TokenEndpoint;
    }

    static async Task<JObject> RequestAccessTokenAsync(TokenClient tokenClient, string scope)
    {
      var tokenResponse = await tokenClient.RequestClientCredentialsAsync(scope);
      if (tokenResponse.IsError)
      {
        throw new InvalidOperationException(tokenResponse.Error);
      }

      return tokenResponse.Json;
    }

    async Task<string> MakeApiCallAsync(string accessToken)
    {
      var client = new HttpClient();
      client.SetBearerToken(accessToken);

      var response = await client.GetAsync(_apiServerUrl);
      if (!response.IsSuccessStatusCode)
      {
        throw new InvalidOperationException(response.StatusCode + response.ReasonPhrase);
      }

      return await response.Content.ReadAsStringAsync();
    }
  }
}