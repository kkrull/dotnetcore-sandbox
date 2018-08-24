using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cli
{
  class Program
  {
    static void Main(string[] args)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      var configuration = builder.Build();
      Console.WriteLine($"source: {configuration["source"]}");
    }
  }
}
