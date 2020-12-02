using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wsei.ExchangeThings.Web.Database;
using Wsei.ExchangeThings.Web.Filters;
using Wsei.ExchangeThings.Web.Models;
using Wsei.ExchangeThings.Web.Models.Validation;

namespace Wsei.ExchangeThings.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(config =>
            {
                config.Filters.Add<MyCustomActionFilter>();
            }).AddFluentValidation();

            services.AddTransient<IValidator<ItemModel>, ItemModelValidator>();

            services.AddDbContext<ExchangesDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("ExchangeThings"))
            );

            services.AddTransient<MyCustomActionFilter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
