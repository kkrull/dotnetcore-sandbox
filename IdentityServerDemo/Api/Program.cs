using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Api
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
        .UseUrls("http://localhost:5001")
        .UseStartup<Startup>()
        .Build();
  }
}