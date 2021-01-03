using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wsei.ExchangeThings.Web.Auth;
using Wsei.ExchangeThings.Web.Database;
using Wsei.ExchangeThings.Web.Filters;
using Wsei.ExchangeThings.Web.Models;
using Wsei.ExchangeThings.Web.Models.Validation;
using Wsei.ExchangeThings.Web.Services;

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

            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    // just to demonstrate CSRF, bring back old behavior
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    // to demonstrate XSS and cookie theft
                    options.Cookie.HttpOnly = false;
                    options.LoginPath = "/auth/login";
                });

            services.AddAuthorization(options => options.AddPolicy(AuthConsts.AuthenticatedScheme, policy =>
            {
                policy.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
            }));

            services.AddTransient<MyCustomActionFilter>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddTransient<ExchangeItemsRepository>();
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

            app.UseAuthentication();

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
