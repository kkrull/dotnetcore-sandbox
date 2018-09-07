# ASP.NET Core Configuration

## Raison d'être

Runtime configuration of ASP.NET Core applications with `appsettings.json`.

It's all fun and games until _somebody tries to load Razor Pages from the source assembly_ while also 
_applying runtime configuration from the test assembly_.

Start with the [.NET Core Document][asp-config], then

    $ dotnet test Cli.Test/Cli.Test.csproj #Run in-process tests in xUnit
    $ ./run-tests.py #Run out of process tests with Python to be really sure that stuff is working


[asp-config]: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.0&tabs=basicconfiguration


## Environment setup (OSX)

Install .NET Core 2.1 by [downloading it from Microsoft][dotnet-core-installer].
Please note that - for some reason - the installation from Homebrew does not work.  
So it has to be installed the old-fashioned way, for now.

After that, you should be able to do something like the following:

    $ dotnet --version
    2.1.400

You may also need to install Mono in order get the XSP server for running ASP.NET applications.
Once again, it needs to be installed [the hard way][mono-installer].

Finally, this may help in removing annoying popup notifications every time a `dotnet` process fails (which will be often)

    $ defaults write com.apple.CrashReporter UseUNC 1

[dotnet-core-installer]: https://www.microsoft.com/net/download
[mono-installer]: http://www.mono-project.com/download/stable


## Is it working?

When everything has all been set up, you should be able to do the following:

    asp-config$ ./run-tests.py #Run all the tests in all the assemblies, from various paths
    asp-config/Cli.Test$ dotnet test #Run xUnit tests in the Cli.Test assembly
    asp-config/Web.Test$ dotnet test #Run xUnit tests in the Web.Test assembly
    asp-config/Web.Test$ dotnet watch test #Continually run xUnit tests in the Web.Test assembly
