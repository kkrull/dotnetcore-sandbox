using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Config;

namespace Web
{
  public class Startup
  {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
      _config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddSingleton<IConfigureServers>(
        new StaticConfiguration { Source = _config.GetValue<string>("Source") });
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
        app.UseDeveloperExceptionPage();
      else
        app.UseExceptionHandler("/Error");
    }
  }
}
