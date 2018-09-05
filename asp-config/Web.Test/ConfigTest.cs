using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Web.Test
{
  public class ConfigTest : IDisposable
  {
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public ConfigTest()
    {
      var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false);

      var hostBuilder = new WebHostBuilder()
        .UseConfiguration(config.Build())
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

    [Fact(DisplayName = "it loads configuration files in the test assembly")]
    public async Task UsesTestConfiguration()
    {
      var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/config/appsettings.json/source"));
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);

      var responseBody = await response.Content.ReadAsStringAsync();
      Assert.Equal("Web.Test", responseBody);
    }
  }
}
