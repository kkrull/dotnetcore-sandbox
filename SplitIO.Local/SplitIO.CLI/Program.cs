using System;
using Splitio.Services.Client.Classes;

namespace SplitIO.CLI
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine($"Arguments[{args.Length}]:");
      var localhostFilePath = args[0];
      Console.WriteLine($"- localhostFilePath: {localhostFilePath}");

      var config = new ConfigurationOptions();
      config.Ready = 10000;
      config.LocalhostFilePath = localhostFilePath;
      var factory = new SplitFactory("localhost", config);
      var sdk = factory.Client();
    }
  }
}