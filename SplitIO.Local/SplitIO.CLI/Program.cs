using System;
using Common.Logging.Simple;
using Splitio.Services.Client.Classes;

namespace SplitIO.CLI
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var localhostFilePath = args[0];
      Console.WriteLine($"localhostFilePath: {localhostFilePath}");

      var client = new LocalhostClient(localhostFilePath, new NoOpLogger());
      
      var factory = new SplitFactory("localhost", new ConfigurationOptions
      {
        LocalhostFilePath = localhostFilePath,
        Ready = 10000
      });

      Console.WriteLine();
      foreach (var splitName in factory.Manager().SplitNames())
      {
        Console.WriteLine($"Split: {splitName}");
      }

//      var client = factory.Client();
      var treatment = client.GetTreatment("id", "testing_split_on");
      Console.WriteLine();
      Console.WriteLine($"Treatment: {treatment}");
    }
  }
}
