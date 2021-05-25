using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcApp1
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //ConfigureIdentityServer(services);
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    options.Authority = "https://localhost:5000/";
                    options.ClientId = "mvc.client";
                    options.ClientSecret = "secret";
                    options.Scope.Add("profile");
                    options.Scope.Add("openid");
                    options.Scope.Add("email");
                    options.Scope.Add("office");
                    options.Scope.Add("phone");
                    options.Scope.Add("api1");
                    options.ResponseType = "code id_token";
                    options.SaveTokens = true;
                });
        }

        //private void ConfigureIdentityServer(IServiceCollection services)
        //{
        //    var builder = services.AddAuthentication(options => SetAuthenticationOptions(options));
        //    builder.AddCookie();
        //    builder.AddOpenIdConnect(options => SetOpenIdConnectOptions(options));
        //}

        //private void SetOpenIdConnectOptions(OpenIdConnectOptions options)
        //{
        //    //options.Authority = "https://localhost:5000/";
        //    //options.ClientId = "mvcapp1";
        //    //options.RequireHttpsMetadata = false;
        //    //options.Scope.Add("profile");
        //    //options.Scope.Add("openid");
        //    //options.Scope.Add("webapiapp1");
        //    ////options.Scope.Add("office");
        //    //options.ResponseType = "code id_token";
        //    //options.SaveTokens = true;
        //    //options.ClientSecret = "0b4168e4-2832-48ea-8fc8-7e4686b3620b";

        //    options.Authority = "https://localhost:5000/";
        //    options.ClientId = "mvc.client";
        //    options.RequireHttpsMetadata = true;
        //    options.ResponseType = "id_token";
        //    options.SignInScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;

        //    options.Scope.Add("profile");
        //    options.Scope.Add("openid");
        //    options.Scope.Add("email");
        //    options.Scope.Add("office");
        //    options.Scope.Add("phone");

        //    //options.ResponseType = "code id_token";
        //    options.UsePkce = false;

        //    options.SaveTokens = true;
        //    options.ClientSecret = "0b4168e4-2832-48ea-8fc8-7e4686b3620b";


        //}

        //private void SetAuthenticationOptions(AuthenticationOptions options)
        //{
        //    options.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults
        //        .AuthenticationScheme;
        //    options.DefaultChallengeScheme = OpenIdConnectDefaults
        //        .AuthenticationScheme;
        //}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            //app.UseAuthorization();
            //app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
