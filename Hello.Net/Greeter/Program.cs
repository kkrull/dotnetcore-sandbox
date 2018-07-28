using System;
using System.Collections.Generic;

namespace Greeter
{
  class Program
  {
    static void Main(string[] args)
    {
      var greeter = new Greeter();
      Console.WriteLine(greeter.Greet());
    }
  }

  public class Greeter
  {
    public string Greet()
    {
      return Greet("World");
    }

    public string Greet(string name)
    {
      return string.Format("Hello {0}!", name);
    }
  }
}