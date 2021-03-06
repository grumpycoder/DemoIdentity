using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApiApp1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5000/";
                options.Audience = "api1";
                options.RequireHttpsMetadata = true;
            });

            //services.AddAuthorization();
            //services.AddAuthentication();

            //ConfigureIdentityServer(services);
        }

        //private void ConfigureIdentityServer(IServiceCollection services)
        //{
        //    var builder = services.AddAuthentication(options =>
        //         options.DefaultScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme
        //         );

        //    builder.AddJwtBearer(options => SetJwtBearerOptions(options));

        //}

        //private void SetJwtBearerOptions(JwtBearerOptions options)
        //{
        //    options.Authority = "https://localhost:5000/";
        //    options.Audience = "webapiapp1";
        //    options.RequireHttpsMetadata = true;
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthentication();


            //app.UseJwtBearerAuthentication(new JwtBearerOptions()
            //{
            //    Authority = "https://localhost:5000/",
            //    Audience = "api1",
            //    RequireHttpsMetadata = true
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
