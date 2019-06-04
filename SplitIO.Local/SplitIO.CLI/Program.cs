using System;
using Splitio.Services.Client.Classes;

namespace SplitIO.CLI
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var localhostFilePath = args[0];
      Console.WriteLine($"localhostFilePath: {localhostFilePath}");

      var factory = new SplitFactory("localhost", new ConfigurationOptions
      {
        LocalhostFilePath = localhostFilePath,
        Ready = 10000
      });
      
      var sdk = factory.Client();
      var treatment = sdk.GetTreatment("anybody", "my_feature");
      Console.WriteLine();
      Console.WriteLine($"Treatment: {treatment}");
    }
  }
}
