using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Config;
using Web.Test;

namespace Web
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      //TODO KDK: Read configuration from JSON
      services
        .AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddSingleton<IConfigureServers>(
        new StaticConfiguration { Source = "Web.Test" });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      ConfigureExceptionPage(app, env);
      app.UseStaticFiles();
      app.UseMvc();
    }

    private static void ConfigureExceptionPage(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }
    }
  }
}
