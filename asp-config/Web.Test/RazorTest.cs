using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Web.Test
{
  public class RazorTest
  {
    private TestServer _server;
    private HttpClient _client;

    public RazorTest()
    {
//      var config = new ConfigurationBuilder()
//        .AddJsonFile("appsettings.json");

      var hostBuilder = new WebHostBuilder()
//        .UseContentRoot()
        .UseStartup<Startup>();
      _server = new TestServer(hostBuilder);
      _client = _server.CreateClient();
    }

    [Fact(DisplayName = "it loads Razor pages from the Web assembly directory")]
    public async Task LoadPagesFromContentRoot()
    {
      var indexResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/"));
      Assert.Equal(HttpStatusCode.OK, indexResponse.StatusCode);
    }

    [Fact(DisplayName = "it responds with 404 Not Found, for a path without a route")]
    public async Task RespondsNotFound()
    {
      var indexResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/bogus"));
      Assert.Equal(HttpStatusCode.NotFound, indexResponse.StatusCode);
    }
  }
}
