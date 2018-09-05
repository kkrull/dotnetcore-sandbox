using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddMvc()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
