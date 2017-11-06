using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using App.Db.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.Webpack;
using App.Db.Identity;
using Microsoft.AspNetCore.Identity;
using App.Core.Interfaces;

namespace App.Web
{
  public class Startup
  {
    private IServiceCollection _services;

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AppDbContext>(options =>
      {
        try
        {
          options.UseInMemoryDatabase("App");
          //options.UseSqlServer(Configuration.GetConnectionString("AppConnection"));
        }
        catch (System.Exception ex)
        {
          var message = ex.Message;
        }
      });

      services.AddDbContext<AppIdentityDbContext>(options => options.UseInMemoryDatabase("Identity"));
      //options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));

      services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

      services.ConfigureApplicationCookie(options =>
      {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.LoginPath = "/Account/Signin";
        options.LogoutPath = "/Account/Signout";
      });

      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
      services.AddMemoryCache();
      services.AddMvc();

      _services = services;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true,
          ReactHotModuleReplacement = true
        });
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();
      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapSpaFallbackRoute(
            name: "spa-fallback",
            defaults: new { controller = "Home", action = "Index" });
      });
    }
  }
}