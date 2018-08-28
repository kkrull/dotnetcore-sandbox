using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cli
{
  public class Program
  {
    private IConfiguration _config;

    static void Main(string[] args)
    {
      var program = new Program().Configure();
      Console.WriteLine($"{program.Source}");
    }

    public Program Configure()
    {
      var assemblyPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
      var sourcePath = ClosestAncestorWithFile(assemblyPath.Parent, "appsettings.json");

      var builder = new ConfigurationBuilder()
        .SetBasePath(sourcePath.FullName)
        .AddJsonFile("appsettings.json");

      _config = builder.Build();
      return this;
    }

    private static DirectoryInfo ClosestAncestorWithFile(DirectoryInfo initialDirectory, string globPattern)
    {
      for (var current = initialDirectory; current != null; current = current?.Parent)
      {
        var matchingFiles = current.GetFiles(globPattern);
        if (matchingFiles.Length == 1)
          return current;

        if (matchingFiles.Length > 1)
        {
          throw new ArgumentException(
            $"Ancestor {current} of {initialDirectory} contains multiple files matching {globPattern}");
        }
      }

      throw new ArgumentException(
        $"No ancestor of {initialDirectory.FullName} contains a single file matching {globPattern}");
    }

    public string Source => _config["source"];
  }
}
