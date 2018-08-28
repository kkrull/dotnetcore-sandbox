using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MvcClient
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    static IWebHost BuildWebHost(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseEnvironment("Development")
        .UseUrls("http://localhost:5002")
        .UseStartup<Startup>()
        .Build();
  }
}