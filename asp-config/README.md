# ASP.NET Core Configuration

Runtime configuration of ASP.NET Core applications with `appsettings.json`.

It's all fun and games until _somebody tries to load Razor Pages from the source assembly_ while also _applying runtime
configuration from the test assembly_.

Start with the [.NET Core Document][asp-config], then

    $ dotnet test Cli.Test/Cli.Test.csproj #Run in-process tests in xUnit
    $ ./run-tests.py #Run out of process tests with Python to be really sure that stuff is working


[asp-config]: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.0&tabs=basicconfiguration
