using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cli
{
  class Program
  {
    static void Main(string[] args)
    {
      var assemblyPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
      var sourcePath = ClosestAncestorWithFile(assemblyPath.Parent, "appsettings.json");

      var builder = new ConfigurationBuilder()
        .SetBasePath(sourcePath.FullName)
        .AddJsonFile("appsettings.json");

      var configuration = builder.Build();
      Console.WriteLine($"{configuration["source"]}");
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
  }
}
