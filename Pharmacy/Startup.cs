using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pharmacy.Domain.Entites;
using Pharmacy.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Antiforgery;
using Pharmacy.Application.Middlewares;
using Pharmacy.Api.ServicesConfiguration;
using Serilog;
using FluentValidation.AspNetCore;
using Pharmacy.Application.Common.DTO.In.Auth.Register;
using Azure.Storage.Blobs;

namespace Pharmacy
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
            services.AddCors();

            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddAntiforgery(options => { options.HeaderName = "x-xsrf-token"; });

            services.AddMvc()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterDtoValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pharmacy API",
                    Description = "Test API with ASP .NET CORE 3.0"
                });

            });
            services.AddControllers();

            services.AddDbContext<PharmacyContext>(cfg =>
            {
                cfg.UseSqlServer(
                    Configuration.GetConnectionString("SqlServerConnectionString"));
            });

            services.AddIdentity<User, IdentityRole>(
                  options =>
                  {
                      options.SignIn.RequireConfirmedAccount = true;
                      options.User.RequireUniqueEmail = true;
                      options.Password.RequireNonAlphanumeric = false;
                  })
                    .AddEntityFrameworkStores<PharmacyContext>()
                    .AddDefaultTokenProviders();

            services.AddSingleton(s => new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorage")));

            AuthenticationConfiguration.RegisterAuthenticationService(services, Configuration);
            ServicesRegistration.RegistereApplicationServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

            app.UseSerilogRequestLogging();
            app.UseMiddleware<TokenMiddleware>();
            app.UseAuthentication();
            //app.UseXsrfProtection(antiforgery);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
