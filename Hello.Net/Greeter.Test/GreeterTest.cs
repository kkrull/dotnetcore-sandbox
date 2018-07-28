using System;
using Xunit;

namespace Greeter.Test
{
  public class GreeterTest
  {
    [Fact]
    public void GreetGivenNoNameSaysHelloWorld()
    {
      var greeter = new Greeter();
      Assert.Equal("Hello World!", greeter.Greet());
    }
    
    [Fact]
    public void GreetGivenANameGreetsThatPerson()
    {
      var greeter = new Greeter();
      Assert.Equal("Hello George!", greeter.Greet("George"));
    }
  }
}