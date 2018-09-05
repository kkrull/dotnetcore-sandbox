using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Web.Test
{
  public class RazorTest : IDisposable
  {
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public RazorTest()
    {
      //TODO KDK: Add a route that returns a configuration value, so we can test which appsettings.json is being used
//      var config = new ConfigurationBuilder()
//        .AddJsonFile("appsettings.json");

      var hostBuilder = new WebHostBuilder()
        .UseContentRoot("../../../../Web")
        .UseStartup<Startup>();
      _server = new TestServer(hostBuilder);
      _client = _server.CreateClient();
    }

    public void Dispose()
    {
      _client.Dispose();
      _server.Dispose();
    }

    [Fact(DisplayName = "it responds with 404 Not Found, for a path without a route")]
    public async Task RespondsNotFound()
    {
      var indexResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/bogus"));
      Assert.Equal(HttpStatusCode.NotFound, indexResponse.StatusCode);
    }

    [Fact(DisplayName = "it loads static files from wwwroot/")]
    public async Task LoadStaticFiles()
    {
      var indexResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/favicon.ico"));
      Assert.Equal(HttpStatusCode.OK, indexResponse.StatusCode);
    }

    [Fact(DisplayName = "it loads Razor pages from the Web assembly")]
    public async Task LoadRazorPages()
    {
      var indexResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/"));
      Assert.Equal(HttpStatusCode.OK, indexResponse.StatusCode);
    }
  }
}
