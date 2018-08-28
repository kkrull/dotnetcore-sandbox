using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IdentityServer
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    static IWebHost BuildWebHost(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseEnvironment("Development")
        .UseUrls("http://localhost:5000")
        .UseStartup<Startup>()
        .Build();
  }
}