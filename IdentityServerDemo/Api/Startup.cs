using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
  class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvcCore()
        .AddAuthorization()
        .AddJsonFormatters();
      
      services.AddAuthentication("Bearer")
        .AddIdentityServerAuthentication(options =>
        {
          //Match IdentityServer's URL and an item in the registered client's AllowedScopes
          options.ApiName = "api1";
          options.Authority = "http://localhost:5000";
          options.RequireHttpsMetadata = false;
        });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseAuthentication();
      app.UseMvc();
    }
  }
}